using DAN_L_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_L_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for Songs.xaml
    /// </summary>
    public partial class Songs : Window
    {
        public Songs()
        {
            InitializeComponent();
            this.DataContext = new AllSongsViewModel(this);
        }
    }
}
