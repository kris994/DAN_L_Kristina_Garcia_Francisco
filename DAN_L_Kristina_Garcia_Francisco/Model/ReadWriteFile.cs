using DAN_L_Kristina_Garcia_Francisco.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DAN_L_Kristina_Garcia_Francisco.Model
{
    /// <summary>
    /// Reads or Writes from the file
    /// </summary>
    class ReadWriteFile
    {
        /// <summary>
        /// Writes the reproduction to file
        /// </summary>
        public void WriteReproductionToFile(tblSong song)
        {
            int? milliseconds = song.SongSeconds * 1000 + song.SongMinutes * 60000 + song.SongHours * 3600000;
            string file = "MyMusic" + LoggedUser.CurrentUser.Username + ".txt";
            string line = "Currently running song name: " + song.SongName + ", author: " + song.SongAuthor + "\n\tSong started playing at: " 
                + DateTime.Now.ToString("HH:mm:ss");
            AllSongsViewModel.printing = true;
            using (StreamWriter streamWriter = new StreamWriter(file, append: true))
            {
                streamWriter.WriteLine(line);
                Thread.Sleep((int)milliseconds);
                streamWriter.WriteLine("\tSong finished at: " + DateTime.Now.ToString("HH:mm:ss"));
            }
            AllSongsViewModel.printing = false;
        }
    }
}
