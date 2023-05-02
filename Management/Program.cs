using TaskManagementApplication.Presentation;
using TaskManagementApplication.Utils;

class Program
{
    static void Main(string[] args)
    {
        ColorCode.DefaultCode("Welcome");
        Start.Run();
    }
}

//LOGIC
//user accesses the application using their email id and password -> -> done
//all internal works related to the user will happen on their userid -> db and controller -> done
//but the info displayed to the user will happen on their names -> presentation -> done