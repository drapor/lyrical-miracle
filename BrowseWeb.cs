using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using LyricalMiracle.Entities;
using LyricalMiracle.BusinessLogic;

namespace LyricalMiracle
{
    public class BrowseWeb
    {
        bool rebootAfter = false;
        TrackLogic trackLogicInstance = new TrackLogic();
        public void GetWebLyrics()
        {
            List<Artist> Everything = new List<Artist>();


            //trackLogicInstance.InsertEverything(Everything);

        }

        public void extract()
        {
            //List<Artist> Artists = new List<Artist>();
            //HtmlWeb hw = new HtmlWeb();

            //for (char c = 'c'; c <= 'z'; c++)
            //{
            //    HtmlDocument doc = hw.Load("http://www.azlyrics.com/" + c +".html");

            //    foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//div[@class='artists fl']//a[@href] | //div[@class='artists fr']//a[@href]"))
            //    //foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//div[@class='artists fl']//a[@href]"))
            //    {
            //        foreach (var item in link.Attributes)
            //        {
            //            if (!item.Value.Contains("http") && item.Value.Contains("/"))
            //            {
            //                Artist artist = new Artist();
            //                artist.ID = Guid.NewGuid();
            //                artist.Link = item.Value;

            //                try
            //                {
            //                    if (artist.Link == "d/disturbed.html" || rebootAfter)
            //                    {
            //                        rebootAfter = true;
            //                        HtmlDocument docArtist = hw.Load(@"http://www.azlyrics.com/" + artist.Link);
            //                        HtmlNode albumNameNode = docArtist.DocumentNode.SelectSingleNode("//div[@id='inn']//h2");

            //                        artist.Name = albumNameNode.InnerText;
            //                        int indexArtistName = artist.Name.IndexOf(" LYRICS");
            //                        artist.Name = artist.Name.Substring(0, indexArtistName);

            //                        HtmlNodeCollection songNodes = docArtist.DocumentNode.SelectNodes("//div[@id='inn']//a[starts-with(@href, '../lyrics')]");
            //                        HtmlNodeCollection albumNodes = docArtist.DocumentNode.SelectNodes("//div[@id='inn']//div[@id='listAlbum']/*");

            //                        if (albumNodes != null)
            //                        {
            //                            foreach (HtmlNode albumChildNode in albumNodes)
            //                            {
            //                                if (albumChildNode.Name == "div")
            //                                {
            //                                    Album album = new Album();
            //                                    album.ID = Guid.NewGuid();
            //                                    album.Name = albumChildNode.InnerText;

            //                                    if (album.Name == "other songs:")
            //                                    {
            //                                    }
            //                                    else if (album.Name.StartsWith("EP:"))
            //                                    {
            //                                        album.Name = album.Name.Substring(5);
            //                                        album.Name = album.Name.Substring(0, album.Name.IndexOf("\""));
            //                                    }
            //                                    else if (album.Name.StartsWith("soundtrack:"))
            //                                    {
            //                                        album.Name = album.Name.Substring(13);
            //                                        album.Name = album.Name.Substring(0, album.Name.IndexOf("\""));
            //                                    }
            //                                    else if (album.Name.StartsWith("compilation:"))
            //                                    {
            //                                        album.Name = album.Name.Substring(14);
            //                                        album.Name = album.Name.Substring(0, album.Name.IndexOf("\""));
            //                                    }
            //                                    else if (album.Name.StartsWith("split:"))
            //                                    {
            //                                        album.Name = album.Name.Substring(8);
            //                                        album.Name = album.Name.Substring(0, album.Name.IndexOf("\""));
            //                                    }
            //                                    else if (album.Name.StartsWith("remix album:"))
            //                                    {
            //                                        album.Name = album.Name.Substring(14);
            //                                        album.Name = album.Name.Substring(0, album.Name.IndexOf("\""));
            //                                    }
            //                                    else if (album.Name.StartsWith("mixtape:"))
            //                                    {
            //                                        album.Name = album.Name.Substring(10);
            //                                        album.Name = album.Name.Substring(0, album.Name.IndexOf("\""));
            //                                    }
            //                                    else if (album.Name.StartsWith("single:"))
            //                                    {
            //                                        album.Name = album.Name.Substring(9);
            //                                        album.Name = album.Name.Substring(0, album.Name.IndexOf("\""));
            //                                    }
            //                                    else if (album.Name.StartsWith("album:"))
            //                                    {
            //                                        album.Name = album.Name.Substring(8);
            //                                        album.Name = album.Name.Substring(0, album.Name.IndexOf("\""));
            //                                    }

            //                                    artist.Albums.Add(album);
            //                                }
            //                                else if (albumChildNode.Name == "a")
            //                                {
            //                                    Song song = new Song();
            //                                    song.ID = Guid.NewGuid();
            //                                    song.Link = albumChildNode.OuterHtml;
            //                                    song.Title = albumChildNode.InnerText;

            //                                    if (song.Link.IndexOf("http") != -1)
            //                                    {
            //                                    }

            //                                    else if (song.Link.IndexOf("google") != -1)
            //                                    {
            //                                    }

            //                                    else
            //                                    {
            //                                        song.Link = song.Link.Substring(11);

            //                                        int index = song.Link.IndexOf("\"");
            //                                        song.Link = song.Link.Substring(0, index);

            //                                        HtmlDocument docLyrics = hw.Load(@"http://www.azlyrics.com/" + song.Link);
            //                                        HtmlNode linkLyricsNameNode = docLyrics.DocumentNode.SelectSingleNode("//div[@style='margin-left:10px;margin-right:10px;']");
            //                                        song.Lyrics = linkLyricsNameNode.InnerText;
            //                                        song.Lyrics = song.Lyrics.Substring(28);

            //                                        song.Lyrics = song.Lyrics.Substring(0, song.Lyrics.Length - 26);
            //                                        artist.Albums[artist.Albums.Count - 1].Songs.Add(song);
            //                                    }
            //                                }
            //                            }
            //                        }
            //                        else if (songNodes != null)
            //                        {
            //                            Album album = new Album();
            //                            album.ID = Guid.NewGuid();
            //                            album.Name = "other songs";

            //                            foreach (HtmlNode node in songNodes)
            //                            {
            //                                Song song = new Song();
            //                                song.ID = Guid.NewGuid();
            //                                song.Title = node.InnerText;
            //                                song.Link = node.OuterHtml;

            //                                song.Link = song.Link.Substring(11);

            //                                int index = song.Link.IndexOf("\"");
            //                                song.Link = song.Link.Substring(0, index);

            //                                HtmlDocument docLyrics = hw.Load(@"http://www.azlyrics.com/" + song.Link);
            //                                HtmlNode linkLyricsNameNode = docLyrics.DocumentNode.SelectSingleNode("//div[@style='margin-left:10px;margin-right:10px;']");
            //                                song.Lyrics = linkLyricsNameNode.InnerText;
            //                                song.Lyrics = song.Lyrics.Substring(28);

            //                                song.Lyrics = song.Lyrics.Substring(0, song.Lyrics.Length - 26);
            //                                album.Songs.Add(song);
            //                            }

            //                            artist.Albums.Add(album);
            //                        }

            //                        try
            //                        {
            //                            trackLogicInstance.InsertEverything(artist);
            //                            Artists.Add(artist);
            //                        }
            //                        catch (Exception ex)
            //                        {

            //                            throw ex;
            //                        }
            //                    }
            //                }
                                
            //                catch (Exception ex)
            //                {
            //                    return Artists;
            //                    throw ex;
            //                }
            //            }
            //        }
            //    }
            //}
            //return Artists;
        }
    }
}
