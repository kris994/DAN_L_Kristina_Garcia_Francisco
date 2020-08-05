using DAN_L_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace DAN_L_Kristina_Garcia_Francisco
{    
    /// <summary>
     /// Class that includes all CRUD functions of the application
     /// </summary>
    class Service
    {
        /// <summary>
        /// Gets all information about users
        /// </summary>
        /// <returns>a list of found users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (AudioDBEntities context = new AudioDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about songs
        /// </summary>
        /// <returns>a list of found songs</returns>
        public List<tblSong> GetAllSongs()
        {
            try
            {
                using (AudioDBEntities context = new AudioDBEntities())
                {
                    List<tblSong> list = new List<tblSong>();
                    list = (from x in context.tblSongs select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all user songs
        /// </summary>
        /// <param name="userID">Gets the current user id</param>
        /// <returns>a list of found songs</returns>
        public List<tblSong> GetAllSongsFromCurrentUser(int userID)
        {
            try
            {
                List<tblSong> list = new List<tblSong>();
                using (AudioDBEntities context = new AudioDBEntities())
                {
                    for (int i = 0; i < GetAllSongs().Count; i++)
                    {
                        if (GetAllSongs()[i].UserID == userID)
                        {
                            list.Add(GetAllSongs()[i]);

                        }
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Searches for a user with the given username, returns 0 if it does not exist
        /// </summary>
        /// <param name="username">Checks if the user with that Username exists</param>
        /// <returns>The found user id</returns>
        public int FindUserByUsername(string username)
        {
            for (int i = 0; i < GetAllUsers().Count; i++)
            {
                if (GetAllUsers()[i].Username == username)
                {
                    return GetAllUsers()[i].UserID;
                }
            }
            return 0;
        }

        /// <summary>
        /// Adds the user
        /// </summary>
        /// <param name="user">the user that is being added</param> 
        /// <returns>a new user</returns>
        public tblUser AddUser(tblUser user)
        {
            try
            {
                using (AudioDBEntities context = new AudioDBEntities())
                {
                    if (FindUserByUsername(user.Username) == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            Username = user.Username,
                            UserPassword = user.UserPassword
                        };

                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        user.UserID = newUser.UserID;

                        return newUser;
                    }
                    else
                    {
                        user.UserID = FindUserByUsername(user.Username);
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Search if song with that ID exists in the song table
        /// </summary>
        /// <param name="songID">Takes the song id that we want to search for</param>
        /// <returns>True if the song exists</returns>
        public bool IsSongID(int songID)
        {
            try
            {
                using (AudioDBEntities context = new AudioDBEntities())
                {
                    int result = (from x in context.tblSongs where x.SongID == songID select x.SongID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Creates a song
        /// </summary>
        /// <param name="song">the song that is being added</param>
        /// <returns>a new song</returns>
        public tblSong AddSong(tblSong song)
        {
            try
            {
                using (AudioDBEntities context = new AudioDBEntities())
                {
                    if (song.SongID == 0)
                    {
                        tblSong newSong = new tblSong
                        {
                            SongName = song.SongName,
                            SongAuthor = song.SongAuthor,
                            SongSeconds = song.SongSeconds,
                            SongMinutes = song.SongMinutes,
                            SongHours = song.SongHours,
                            UserID = LoggedUser.CurrentUser.UserID
                        };

                        context.tblSongs.Add(newSong);
                        context.SaveChanges();
                        song.SongID = newSong.SongID;

                        return song;
                    }
                    else
                    {
                        return song;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Deletes the song depending if the songID already exists
        /// </summary>
        /// <param name="songID">the song that is being deleted</param>
        /// <returns>list of song</returns>
        public void DeleteSong(int songID)
        {
            try
            {
                using (AudioDBEntities context = new AudioDBEntities())
                {
                    bool isSong = IsSongID(songID);
                    if (isSong == true)
                    {
                        tblSong songToDelete = (from r in context.tblSongs where r.SongID == songID select r).First();

                        context.tblSongs.Remove(songToDelete);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete the song");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
