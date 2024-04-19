using CommunityToolkit.Mvvm.ComponentModel;
using Homework_11_CustomTable.Models;

namespace Homework_11_CustomTable.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private User _userData = new User
    {
        id = 0,
        name = "Name123",
        username = "UsernameTest",
        email = "www@gmail.com",
        address = new Address()
        {
            city = "Novosibirsk",
            geo = new Geo()
            {
                lat = "123",
                lng = "321",
            }
        },
        phone = null,
        website = null,
        company = null
    };
}
