using DAN_L_Kristina_Garcia_Francisco.Commands;
using DAN_L_Kristina_Garcia_Francisco.Model;
using DAN_L_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_L_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Main Window view model
    /// </summary>
    class AllSongsViewModel : BaseViewModel
    {
        Songs songWindow;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with AllSongsViewModel param
        /// </summary>
        /// <param name="Songs">opens the all Songs window</param>
        public AllSongsViewModel(Songs songsOpen)
        {
            songWindow = songsOpen;
            SongList = service.GetAllSongs().ToList();
            AllCurrentUsersSongs = service.GetAllSongsFromCurrentUser(LoggedUser.CurrentUser.UserID).ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of songs
        /// </summary>
        private List<tblSong> songList;
        public List<tblSong> SongList
        {
            get
            {
                return songList;
            }
            set
            {
                songList = value;
                OnPropertyChanged("SongList");
            }
        }

        /// <summary>
        /// List of current users songs
        /// </summary>
        private List<tblSong> allCurrentUsersSongs;
        public List<tblSong> AllCurrentUsersSongs
        {
            get
            {
                return allCurrentUsersSongs;
            }
            set
            {
                allCurrentUsersSongs = value;
                OnPropertyChanged("AllCurrentUsersSongs");
            }
        }

        /// <summary>
        /// Specific Song
        /// </summary>
        private tblSong song;
        public tblSong Song
        {
            get
            {
                return song;
            }
            set
            {
                song = value;
                OnPropertyChanged("Song");
            }
        }     
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to delete the song
        /// </summary>
        private ICommand deleteSong;
        public ICommand DeleteSong
        {
            get
            {
                if (deleteSong == null)
                {
                    deleteSong = new RelayCommand(param => DeleteSongExecute(), param => CanDeleteSongExecute());
                }
                return deleteSong;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeleteSongExecute()
        {
            // Checks if the user really wants to delete the song
            var result = MessageBox.Show("Are you sure you want to delete the song?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Song != null)
                    {
                        int songID = Song.SongID;
                        service.DeleteSong(songID);
                        SongList = service.GetAllSongs().ToList();
                        AllCurrentUsersSongs = service.GetAllSongsFromCurrentUser(LoggedUser.CurrentUser.UserID).ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if the song can be deleted
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeleteSongExecute()
        {
            if (SongList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to add a new song
        /// </summary>
        private ICommand addNewSong;
        public ICommand AddNewSong
        {
            get
            {
                if (addNewSong == null)
                {
                    addNewSong = new RelayCommand(param => AddNewSongExecute(), param => CanAddNewSongExecute());
                }
                return addNewSong;
            }
        }

        /// <summary>
        /// Executes the add Song command
        /// </summary>
        private void AddNewSongExecute()
        {
            try
            {
                AddSong addSong = new AddSong();
                addSong.ShowDialog();
                if ((addSong.DataContext as AddSongViewModel).IsUpdateSong == true)
                {
                    SongList = service.GetAllSongs().ToList();
                    AllCurrentUsersSongs = service.GetAllSongsFromCurrentUser(LoggedUser.CurrentUser.UserID).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new song
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewSongExecute()
        {
            return true;
        }
        
        /// <summary>
        /// Command that logs off the user
        /// </summary>
        private ICommand logoff;
        public ICommand Logoff
        {
            get
            {
                if (logoff == null)
                {
                    logoff = new RelayCommand(param => LogoffExecute(), param => CanLogoffExecute());
                }
                return logoff;
            }
        }

        /// <summary>
        /// Executes the logoff command
        /// </summary>
        private void LogoffExecute()
        {
            try
            {
                Login login = new Login();
                songWindow.Close();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to logoff
        /// </summary>
        /// <returns>true</returns>
        private bool CanLogoffExecute()
        {
            return true;
        }
        #endregion
    }
}
