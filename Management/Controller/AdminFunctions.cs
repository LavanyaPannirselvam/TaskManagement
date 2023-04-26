using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApplication.Controller.Interface;
using TaskManagementApplication.DataBase;
using TaskManagementApplication.Enumerations;
using TaskManagementApplication.Model;
using TaskManagementApplication.Utils;

namespace TaskManagementApplication.Controller
{
    public class AdminFunctions : IAdminOperations
    {
        private readonly Database _database = Database.GetInstance();

        public string ApproveUser(string email)
        {
            if (_database.IsTemporaryUserAvailable(email))
            {
                if (!_database.IsUserAvailable(email))
                {
                    User temporaryUser = _database.GetUser(email);
                    ApprovedUser user = new(temporaryUser.Name, temporaryUser.Email, temporaryUser.Role, UserApprovalOptions.APPROVED);
                    if (_database.AddUser(user) == Result.SUCCESS)
                    {
                        Notification notification = new("${email}approved, Your userId is : {user.UserId} and password is what you entered during request");
                        temporaryUser.Notifications.Add(notification);
                        user.Notifications.Add(notification);
                        return "User approved";
                    }
                    else
                    {
                        temporaryUser.Notifications.Add(new Notification("Some error occured"));
                        return "Some error occured";
                    }
                }
                else
                {
                    _database.GetUser(email).Notifications.Add(new("Email already exists, try to login to your account"));
                    return "Email already exists";
                }
            }
            else return "Email not present";
        }

        public string ShowNotifications()
        {
            if (_database.Admin.Notifications.Count > 0)
            {
                foreach (Notification msg in _database.Admin.Notifications.Values)
                    ColorCode.DefaultCode(msg.ToString());
                return "";
            }
            else return "You don't have any notification to show";
        }
    }
}
//a user requests for signup & enters the data -> done
//the data gets stored in a temporary dictionary with email as key and user(with just name and role) as value -> done
//after the data stored in temp db,admin gets the notification with the message and email id of the user -> done
//admin should enter the email from the notification to view them for approval -> done
//admin should chk for duplicates in email and should accept / reject the user -> done
//if accepted,the user should get a notification about his/her account credentials in both temporary and permanent user obj -> done
//until it gets accepted,the user is given a dummy userid and password to access only the notification section of him/her -> done


//admin operations -> to view notifications,to accept/reject the user,to delete an user account
//view notification -> done
//approve user -> done
//delete user

//user request -> done
//enter data & get dummy userid and password -> done

//2 different login options for temporary and permanent users -> done, to be checked

