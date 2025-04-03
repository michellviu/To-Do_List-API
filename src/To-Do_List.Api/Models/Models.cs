using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.Api.Models;

public class Response
{
    public string Message { get; set; }
}
public class RequestUserLogin
{
    public string email { get; set; }
    public string password { get; set; }
}

public class RequestUserRegister
{
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
}

public class ResponseLogin
{
    public string token { get; set; }
}

