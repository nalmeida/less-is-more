using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for IContent
/// </summary>
public interface IContent
{
    string Type { get; }
    string CustonHeader();
}
