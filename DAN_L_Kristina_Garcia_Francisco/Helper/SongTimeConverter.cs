using System;
using System.Globalization;
using System.Windows.Data;

namespace DAN_L_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Convertes the id of the song to the time shown
    /// </summary>
    class SongTimeConverter : IValueConverter
    {
        /// <summary>
        /// Converts the parameter value into the item name
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Service service = new Service();
            for (int i = 0; i < service.GetAllSongs().Count; i++)
            {
                if (service.GetAllSongs()[i].SongID == (int)value)
                {
                    if (service.GetAllSongs()[i].SongHours == 0)
                    {
                        return service.GetAllSongs()[i].SongMinutes + ":" + service.GetAllSongs()[i].SongSeconds;
                    }
                    else
                    {
                        return service.GetAllSongs()[i].SongHours + ":" + service.GetAllSongs()[i].SongMinutes + ":" + service.GetAllSongs()[i].SongSeconds;
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Converts back
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
