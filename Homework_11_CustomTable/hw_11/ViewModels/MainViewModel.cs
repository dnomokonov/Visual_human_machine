using CommunityToolkit.Mvvm.ComponentModel;
using hw_11.Entities;

namespace hw_11.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private User _userData = new User
    {
        id = 0,
        name = "NewNeWnamee",
        username = "NewUsers",
        email = "testetst@gmail.com",
        address = new Address()
        {
            city = "Moscow",
            geo = new Geo()
            {   
                lat = "542",
                lng = "1245",
            }
        },
        phone = null,
        website = null,
        company = null
    };

    public MainViewModel() { }
}
