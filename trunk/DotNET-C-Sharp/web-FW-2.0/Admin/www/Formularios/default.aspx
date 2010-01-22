<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server"><% // content place holder do header %></asp:content>

<asp:content ID="Content5" contentplaceholderid="cphBodyProperties" runat="server">class="formularios"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
    <div id="primary_content">
       	<h2>Formulários</h2>
        <p>Itens  de formulário</p>		       
            <p>
                <label>
                    Small Form Field
                </label>
                <input name="textfield1" class="input small" id="textfield1" value="Small..." type="text">
            </p>
            <p>
                <label>
                    Medium Form Field
                </label>
                <input name="textfield2" class="input medium" id="textfield2" value="Medium..." type="text">
            </p>
            <p>
                <label>
                    Large Form Field
                </label>
                <input name="textfield3" class="input large" id="textfield3" value="Large..." type="text">
            </p>
            <p>
                <label>
                    Check Boxes
                </label>
            </p>
            <p>
                <input name="check1" class="checkbox" id="check1" type="checkbox">This is a checkbox 
            </p>
            <p>
                <input name="check2" class="checkbox" id="check2" checked="checked" type="checkbox">This is another checkbox 
            </p>
            <p>
                <label>
                    Radio Buttons
                </label>
                <input name="radio" class="radio" id="radio1" value="radio1" type="radio">This is a radio option 
            </p>
            <p>
                <input name="radio" class="radio" id="radio2" value="radio1" type="radio">Another Radio Option
            </p>
            <p>
                <input name="radio" class="radio" id="radio3" value="radio3" type="radio">Last Radio Option 
            </p>
            <p>
                <label>
                    Drop Down (Combo) List
                </label>
                <select name="combo" class="select input">
                    <option selected="selected" value="Option 1">Option 1</option>
                    <option value="Option 2">Option 2</option>
                    <option value="Option 3">Option 3</option>
                    <option value="Option 4">Option 4</option>
                    <option value="Option 5">Option 5</option>
                </select>
            </p>
            <p>
                <label>
                    This is a text area...
                </label>
                <textarea name="textarea" cols="50" rows="7" class="styled_textarea">
                    Text area for lots of lines of text...
                </textarea>
            </p>
            <p>
                <input class="button" value="Submit" type="button">
            </p>
        
               
    </div>
    <!--  end div #primary_content -->
</asp:content>
