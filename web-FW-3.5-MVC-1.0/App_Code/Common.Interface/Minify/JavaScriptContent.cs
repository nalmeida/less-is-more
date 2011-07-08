using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for JavaScriptContent
/// </summary>
public class JavaScriptContent : IContent
{

    #region IContent Members

    public string Type { get { return "text/javascript"; } }

    public string CustonHeader()
    {
        return @"window['Common'] = window['Common'] || {Util: { 
                    Root: '" + Common.Util.Root + @"',
                    AssetsRoot: '" + Common.Util.AssetsRoot + @"', 
                    GlobalPath: '" + Common.Util.GlobalPath + @"', 
                    LanguagePath: '" + Common.Util.LanguagePath + @"',
                    UploadsRoot: '" + Common.Util.UploadsRoot + @"',
                    GlobalUploadPath: '" + Common.Util.GlobalUploadPath + @"',
                    LanguageUploadPath: '" + Common.Util.LanguageUploadPath + "'}; \n";
    }

    #endregion
}
