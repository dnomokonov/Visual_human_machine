using System;
using System.Collections.Generic;
using Homework_8_MVVM.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;

namespace Homework_8_MVVM.ViewModels.Pages
{
	public class DataGridPageViewModel : BaseViewModel
    {
        private ObservableCollection<Users> users;
        public ObservableCollection<Users> Users
        {
            get => users;
            set => this.RaiseAndSetIfChanged(ref users, value);
        }

        public HttpClientViewModel HttpClientViewModel { get; set; }

        public DataGridPageViewModel()
        {
            users = new ObservableCollection<Users>();
            HttpClientViewModel = new HttpClientViewModel();

            LoadData();
        }

        public async Task LoadData()
        {
            List<Users>? users = await HttpClientViewModel.GetWebDataUsers();
            if (users != null)
            {
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
        }
    }
}