class Account
{
    private decimal _balance;
    private List<INotifyer> _notifyers;

    public Account() 
    {
        _balance = 0;
        _notifyers = new List<INotifyer>();
    }
    public Account(in decimal balance) 
    {
        _balance = balance;
        _notifyers = new List<INotifyer>();
    }
    public void AddNotifyer(INotifyer notifyer) 
    {
        _notifyers.Add(notifyer);
    }
    public void ChangeBalance(in decimal balance)
    {
        this._balance = balance;
        Notification();
    }
    public decimal Balance 
    {
        get { return _balance; }
    }
    private void Notification() 
    {
        foreach (var notifi in _notifyers)
        {
            notifi.Notify(this._balance);
        }
    }
}

class SMSLowBalanceNotifyer : INotifyer
{
    private string _phone;
    private decimal _lowBalanceValue;

    public SMSLowBalanceNotifyer(string phone, in decimal lowBalanceValue)
    {
        _phone = phone;
        _lowBalanceValue = lowBalanceValue;
    }
    public void Notify(in decimal balance)
    {
        if (balance < _lowBalanceValue)
        {
            Console.WriteLine($"SMSLowBalanceNotifyer: {balance}");
        } else
        {
            Console.WriteLine("SMSLowBalanceNotifyer");
        }
        
    }
}

class EMailBalanceChangedNotifyer : INotifyer
{
    private string _email;

    public EMailBalanceChangedNotifyer(string email)
    {
        _email = email;
    }

    public void Notify(in decimal balance)
    {
        Console.WriteLine($"EMailBalanceChangedNotifyer: {balance}");
    }
}

interface INotifyer
{
    public void Notify(in decimal balance);
}

class Program
{
    static void Main(string[] arc)
    {
        Account account = new Account();
        SMSLowBalanceNotifyer sms = new SMSLowBalanceNotifyer("+79930044455", 180);
        EMailBalanceChangedNotifyer email = new EMailBalanceChangedNotifyer("email@email.com");

        account.AddNotifyer(sms);
        account.AddNotifyer(email);

        account.ChangeBalance(65);
    }
}