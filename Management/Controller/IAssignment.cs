using ConsoleApp1.Enumeration;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
	public interface IAssignment
	{
		string AssignUser(int projectId,User user);
		string DeassignUser(int projectId,User user);
	}
}