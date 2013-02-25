using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using iTunesLib;
using LyricalMiracle.BusinessLogic;
using System.Windows.Threading;
using LyricalMiracle.Entities;
using System.Security.Cryptography;
using ManagedWinapi.Hooks;
using System.Timers;
using LyricalMiracle.UI;

namespace LyricalMiracle
{
    public partial class MainWindow : Window
    {
        #region Properties

        private User user = new User();
        private Bootstrap BSInstance = new Bootstrap();
        private TrackLogic TLInstance = new TrackLogic();
        private LoginLogic LLInstance = new LoginLogic();
        private UserLogic ULInstance = new UserLogic();

        private iTunesApp itunes = null;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Lyrics selectedLyrics = new Lyrics();

        #endregion

        #region Global

        public MainWindow(User userLogin)
        {
            user = userLogin;
            //_hookID = SetHook(_proc);
            hookProc = new HookProc(LowLevelMouseProc);
            hook = SetWindowsHookEx(WH_MOUSE_LL, hookProc, GetModuleHandle(null), 0);

            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Bootstrap();
        }

        private void Bootstrap()
        {
            Application.Current.MainWindow = this;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Height = System.Windows.SystemParameters.WorkArea.Height;
            this.Width = SystemParameters.WorkArea.Width * 0.2;
            this.Left = System.Windows.SystemParameters.WorkArea.Width - this.Width;
            this.Top = 0;
            this.Topmost = true;

            var Processes = System.Diagnostics.Process.GetProcessesByName("iTunes");

            if (Processes.Any())
            {
                BSInstance.StartBootstrap(new WindowInteropHelper(this).Handle);
            }
            else
            {
                BSInstance.StartBootstrapItunes(new WindowInteropHelper(this).Handle);
            }

            AttachEvents();
            AttachProfil(user);
        }

        private void Lyrical_Closed(object sender, EventArgs e)
        {
            // Remove any handlers from the iTunes COM object.
            itunes.OnPlayerPlayEvent -= new _IiTunesEvents_OnPlayerPlayEventEventHandler(GetTrackLyrics);

            // Release the COM object.
            Marshal.ReleaseComObject(itunes);

            ULInstance.SetConnected(user.ID, false, Enums.StatsType.Disconnect);
        }

        #region Utilitaries

        private void AttachEvents()
        {
            itunes = new iTunesApp();
            itunes.OnPlayerPlayEvent += new _IiTunesEvents_OnPlayerPlayEventEventHandler(GetTrackLyrics);
        }

        private void UILoading(bool loading)
        {
            if (loading)
            {
                LoadingGIF.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => LoadingGIF.Visibility = System.Windows.Visibility.Visible));
            }
            else
            {
                LoadingGIF.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => LoadingGIF.Visibility = System.Windows.Visibility.Hidden));
            }
        }

        private void SetiTunes()
        {

        }

        #endregion

        #endregion

        #region Lyrics

        private void btnLyricsSearch_Click(object sender, RoutedEventArgs e)
        {
            UILoading(true);
        }

        private void tbLyricsSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            SongLyricstbLyricsSearch.Text = string.Empty;
            SongLyricstbLyricsSearch.Foreground = Brushes.Black;
        }

        private void tbLyricsSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            SongLyricstbLyricsSearch.Text = Properties.Resources.SearchLabelText;
            SongLyricstbLyricsSearch.Foreground = Brushes.Gray;
        }

        private void SongAddbtnAddLyrics_Click(object sender, RoutedEventArgs e)
        {
            SongAddlyrics.Dispatcher.BeginInvoke(new Action(() => SongAddlyrics.Visibility = Visibility.Visible));
            SongLyrics.Dispatcher.BeginInvoke(new Action(() => SongLyrics.Visibility = Visibility.Hidden));
            SongAdd.Dispatcher.BeginInvoke(new Action(() => SongAdd.Visibility = Visibility.Hidden));

            Track currentTrack = GetCurrentTrack();
            txtTitle.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => txtTitle.Text = currentTrack.Name));
            txtArtist.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => txtArtist.Text = currentTrack.Artist));
            txtAlbum.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => txtAlbum.Text = currentTrack.Album));
        }

        private void SongAddbtnBack_Click(object sender, RoutedEventArgs e)
        {
            SongMain.Dispatcher.BeginInvoke(new Action(() => SongMain.Visibility = Visibility.Visible));
            SongLyrics.Dispatcher.BeginInvoke(new Action(() => SongLyrics.Visibility = Visibility.Hidden));
            SongAdd.Dispatcher.BeginInvoke(new Action(() => SongAdd.Visibility = Visibility.Hidden));
        }

        #region Utilitaries

        private Track GetCurrentTrack()
        {
            IITTrack currTrack = itunes.CurrentTrack;
            Track currentTrack = new Track();

            if (currTrack != null)
            {

                currentTrack.Name = currTrack.Name;
                currentTrack.Artist = currTrack.Artist;
                currentTrack.Album = currTrack.Album;
                currentTrack.Genre = currTrack.Genre;
                currentTrack.Duration = currTrack.Duration;
                currentTrack.Artwork = currTrack.Artwork;
                currentTrack.BPM = currTrack.BPM;
                currentTrack.BitRate = currTrack.BitRate;
            }

            return currentTrack;
        }

        public void GetTrackLyrics(object currentTrack)
        {
            UILoading(true);
            SongMain.Dispatcher.BeginInvoke(new Action(() => SongMain.Visibility = Visibility.Hidden));
            SongLyrics.Dispatcher.BeginInvoke(new Action(() => SongLyrics.Visibility = Visibility.Visible));

            itunes.OnPlayerPlayEvent -= new _IiTunesEvents_OnPlayerPlayEventEventHandler(GetTrackLyrics);
            itunes.OnPlayerPlayEvent += new _IiTunesEvents_OnPlayerPlayEventEventHandler(GetTrackLyrics);

            Track currTrack = GetCurrentTrack();
            currTrack.Lyrics = TLInstance.GetLyricsPerTrack(currTrack);
            SetGeneralLabels(currTrack);

            if (currTrack.Lyrics == string.Empty)
            {
                SongMain.Dispatcher.BeginInvoke(new Action(() => SongMain.Visibility = Visibility.Hidden));
                SongLyrics.Dispatcher.BeginInvoke(new Action(() => SongLyrics.Visibility = Visibility.Hidden));
                SongAddlyrics.Dispatcher.BeginInvoke(new Action(() => SongAddlyrics.Visibility = Visibility.Hidden));
                SongAdd.Dispatcher.BeginInvoke(new Action(() => SongAdd.Visibility = Visibility.Visible));
            }

            UILoading(false);
        }

        private void SetGeneralLabels(Track currentTrack = null)
        {
            if (currentTrack == null)
            {
                currentTrack = GetCurrentTrack();
            }

            SongLyricslbTitle.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => SongLyricslbTitle.Content = currentTrack.Name));
            SongLyricslbArtist.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => SongLyricslbArtist.Content = currentTrack.Artist));
            SongLyricslbAlbum.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => SongLyricslbAlbum.Content = currentTrack.Album));
            SetLyrics(currentTrack);

        }

        private void SetLyrics(Track currentTrack)
        {
            SongLyricsrtbLyrics.Dispatcher.BeginInvoke(new Action(() => SongLyricsrtbLyrics.Selection.Text = currentTrack.Lyrics));
            SongLyricsrtbLyrics.Dispatcher.BeginInvoke(new Action(() => SongLyricsrtbLyrics.Document.TextAlignment = TextAlignment.Justify));
        }

        private void SetUIState()
        {

        }

        #endregion

        #endregion

        #region Profil

        private void AttachProfil(User user)
        {
            ProfilTab.DataContext = user;
            ProfilLvLyrics.ItemsSource = user.Lyrics;
            ProfilLvMeanings.ItemsSource = user.Meanings;
        }

        protected void ProfilListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Lyrics lyrics = ((ListViewItem)sender).Content as Lyrics;
            Meanings meanings = ((ListViewItem)sender).Content as Meanings;

            if (lyrics != null)
            {
                //Link à une autre place
            }
            else if (meanings != null)
            {
                //Link à une autre place
            }
        }

        #endregion

        /* #region Hook2

        // create a mouse over eventstatic int WH_MOUSE_LL = 14;
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr GetModuleHandle(string moduleName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(IntPtr hhook);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, uint wParam, IntPtr lParam);
        delegate IntPtr HookProc(int nCode, uint wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [Flags]
        private enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }
        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        private HookProc hookProc;
        private IntPtr hook;
        private const int WH_MOUSE_LL = 14;

        IntPtr LowLevelMouseProc(int nCode, uint wParam, IntPtr lParam)
        {
            if (nCode >= 0 && MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
            {
                foreach (IITWindow iTunes in itunes.Windows)
                {
                    //MessageBox.Show(iTunes.Minimized.ToString());
                    //MessageBox.Show(iTunes.Maximized.ToString());
                    //MessageBox.Show(iTunes.Maximizable.ToString());
                    //MessageBox.Show(iTunes.Height.ToString());
                }
            }
            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        #endregion */

    }
}
