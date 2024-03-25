using System;
using System.Collections.Generic;
using ReactiveUI;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Homework_8_MVVM.Models
{
    public class Geo
    {
        public string? Lat { get; set; }
        public string? Lng { get; set; }
    }

    public class Company
    {
        public string? Name { get; set; }
        public string? CatchPhrase { get; set; }
        public string? Bs { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? Suite { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }
        public Geo? Geo { get; set; }
    }

    public partial class Users : ObservableObject
	{
		[ObservableProperty]
		public int id;
		[ObservableProperty]
		public string? name;
        [ObservableProperty]
        public string? username;
        [ObservableProperty]
        public string? email;
        [ObservableProperty]
        public Address? address;
        [ObservableProperty]
        public string? phone;
        [ObservableProperty]
        public string? website;
        [ObservableProperty]
        public Company? company;
	}
}