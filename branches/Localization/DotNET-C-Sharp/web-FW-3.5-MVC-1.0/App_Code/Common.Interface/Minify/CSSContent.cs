using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for CSSContent
/// </summary>
public class CSSContent : IContent
{

    #region IContent Members

    public string Type { get { return "text/css"; } }

    public string CustonHeader()
    {
        return "";
    }

    #endregion
}
