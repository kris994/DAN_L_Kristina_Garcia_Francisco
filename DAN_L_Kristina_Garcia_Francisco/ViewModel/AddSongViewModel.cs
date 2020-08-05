using DAN_L_Kristina_Garcia_Francisco.Commands;
using DAN_L_Kristina_Garcia_Francisco.Model;
using DAN_L_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_L_Kristina_Garcia_Francisco.ViewModel
{
    class AddSongViewModel : BaseViewModel
    {
        AddSong addSong;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with the add song info window opening
        /// </summary>
        /// <param name="addSongOpen">opends the add song window</param>
        public AddSongViewModel(AddSong addSongOpen)
        {
            song = new tblSong();
            addSong = addSongOpen;
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

        /// <summary>
        /// Cheks if its possible to execute the add song command
        /// </summary>
        private bool isUpdateSong;
        public bool IsUpdateSong
        {
            get
            {
                return isUpdateSong;
            }
            set
            {
                isUpdateSong = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new song
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => this.CanSaveExecute);
                }
                return save;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                service.AddSong(Song);
                isUpdateSong = true;

                addSong.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the song
        /// </summary>
        protected bool CanSaveExecute
        {
            get
            {
                return Song.IsValid;
            }
        }
    
        /// <summary>
        /// Command that closes the add worker or edit worker window
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                addSong.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to execute the close command
        /// </summary>
        /// <returns>true</returns>
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
