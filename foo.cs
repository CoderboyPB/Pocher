public class UserDisplayModel : INotifyPropertyChanged
{
    public UserDisplayModel(UserModel userModel)
    {
        Id = userModel.Id;
        Email = userModel.Email;
        Roles = userModel.Roles.Select(r => r.Value).ToList();
    }

    public string Id { get; set; }
    public string Email { get; set; }

    private List<string> _roles;
    public List<string> Roles
    {
        get => _roles; 
        set 
        {
            _roles = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Roles"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RoleList"));
        }
    }

    public string RoleList
    {
        get => string.Join(", ", Roles);
    }

    public event PropertyChangedEventHandler PropertyChanged;
}

// ############################################################################################

public class UserModel
{
    public string Id { get; set; }
    public string Email { get; set; }
    public Dictionary<string, string> Roles { get; set; }
}

// ##############################################################################################

private async Task LoadUsers()
{
    var userList = await _userEndpoint.GetAll();
    var userDisplayList = new List<UserDisplayModel>();
    userList.ForEach(ul => userDisplayList.Add(new UserDisplayModel(ul)));

    Users = new ObservableCollection<UserDisplayModel>(userDisplayList);
}