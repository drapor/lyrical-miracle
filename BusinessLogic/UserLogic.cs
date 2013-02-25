using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyricalMiracle.Data;
using LyricalMiracle.Entities;

namespace LyricalMiracle.BusinessLogic
{
    public class UserLogic
    {
        Exceptions exceptionInstance = new Exceptions();
        UserData userDataInstance = new UserData();
        LyricsLogic lyricsLogicInstance = new LyricsLogic();
        MeaningsLogic meaningsLogicInstance = new MeaningsLogic();
        StatsLogic statsLogicInstance = new StatsLogic();

        /// <summary>
        /// Methode utilisée seulement dans le cas du login. On incremente la table de stats ainsi qu'on set
        /// la session user à true. À ne pas utiliser pour aller chercher un autre user que celui connecté.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            User user = new User();

            try
            {
                user = userDataInstance.GetUser(username);
                user.Lyrics = lyricsLogicInstance.GetLyrics(user.ID);
                user.Meanings = meaningsLogicInstance.GetMeanings(user.ID);

                //Set la colonne Connected à true sur le login
                SetConnected(user.ID, true, Enums.StatsType.Connect);

            }
            catch (Exception ex)
            {
                exceptionInstance.WriteException(Guid.NewGuid(), ex, "UserLogic", "GetUser", DateTime.Now);
            }

            return user;
        }

        public void SetConnected(Guid IDUser, bool state, Enums.StatsType statsType)
        {
            try
            {
                userDataInstance.SetConnected(IDUser, state);

                //Increment la table de stats sur le login
                statsLogicInstance.WriteStats(IDUser, statsType);

            }
            catch (Exception ex)
            {
                exceptionInstance.WriteException(Guid.NewGuid(), ex, "UserLogic", "SetConnected", DateTime.Now);
            }
        }
    }
}
