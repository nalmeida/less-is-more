<%@ Page masterpagefile="~/MasterPage.master" Language="C#" %>

<asp:content contentplaceholderid="cphCustomTitle" runat="server">Módulo Padrão C# 15.7 - utf-8</asp:content>

<asp:content contentplaceholderid="cphCustomHeader" runat="server"><% // content place holder do header %></asp:content>

<asp:content ID="Content5" contentplaceholderid="cphBodyProperties" runat="server">class="home"</asp:content>

<asp:content contentplaceholderid="cphMainContent" runat="server">
    <div id="primary_content">
        <h2>Welcome back <span class="theme_highlight">John Doe</span></h2>
        <p>
            This
            is some introductory text for the Super Skinnable Admin Theme. This
            theme is really simple to skin because it uses very few colours or
            images, so you can add your own icons or use those recommended in the
            helpfile. Enjoy clicking around the theme!
        </p>
			
		<!--  START NOTIFICATION BOXES -->       
        <div class="success">Success - This is a success mssage.</div>
        <div class="fail">Failure - This is a failure mssage.</div>
        <!--  END NOTIFICATION BOXES -->
		
		<h2>List Types</h2>
        <p>There are many types of lists used in web design, here are just a few of them.</p>
        
		<!--  Definition List -->
		<h3>Definition List</h3>
        <dl>
            <dt>Definition List Title</dt>
            <dd>This is a definition list division.</dd>
        </dl>
		
        <!--  Ordered List -->
		<h3>Ordered List</h3>
        <ol>
            <li>List Item 1</li>
            <li>List Item 2</li>
            <li>List Item 3</li>
        </ol>
		
        <!--  Unordered List -->
		<h3>Unordered List</h3>
        <ul>
            <li>List Item 1</li>
            <li>List Item 2</li>
            <li>List Item 3</li>
        </ul>
		
        <!--  START TAB -->
        <div id="tabs" class="tabs">
           
		    <!--  TAB BOXES/HEADINGS -->
            <ul>
                <li class="active"><a href="#tab-1">Tables</a></li>
                <li><a href="#tab-2">Forms</a></li>
                <li><a href="#tab-3">Typography</a></li>
            </ul>
           
		    <!--  START TAB 1 -->
            <div id="tab-1">
                <h3>Tables</h3>
                <p>
                    Here
                    is an example of a styled table. The table is a very important part of
                    any admin theme as we want to be able to visually sift through lots of
                    data aall at once. This table has zebra striping by default.
                </p>
                <table class="tablesorter">
                    <thead>
                        <tr>
                            <th class="header" width="22%">
                                Name
                            </th>
                            <th class="header" width="34%">
                                E-Mail
                            </th>
                            <th class="header" width="25%">
                                Telephone
                            </th>
                            <th class="header" width="19%">
                                Action
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                John Doe
                            </td>
                            <td>
                                <a href="mailto:john.doe@hotmail.com">john.doe@hotmail.com</a>
                            </td>
                            <td>
                                0130 456 890
                            </td>
                            <td>
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                            </td>
                        </tr>
                        <tr class="even">
                            <td>
                                Jackie Ching
                            </td>
                            <td>
                                <a href="mailto:j.ching@yahoo.com">j.ching@yahoo.com</a>
                            </td>
                            <td>
                                0120 546 999
                            </td>
                            <td>
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Samantha Dollock
                            </td>
                            <td>
                                <a href="mailto:sam_dollock@hotmail.com">sam_dollock@hotmail.com</a>
                            </td>
                            <td>
                                0141 654 001
                            </td>
                            <td>
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                            </td>
                        </tr>
                        <tr class="even">
                            <td>
                                Kevin Dallingham
                            </td>
                            <td>
                                <a href="mailto:kevindallingham@gmail.com">kevindallingham@gmail.com</a>
                            </td>
                            <td>
                                0171 123 890
                            </td>
                            <td>
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jamie Tollhurst
                            </td>
                            <td>
                                <a href="#">tollhurst@yahoo.com</a>
                            </td>
                            <td>
                                0121 321 456
                            </td>
                            <td>
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                            </td>
                        </tr>
                        <tr class="even">
                            <td>
                                Simon Wright
                            </td>
                            <td>
                                <a href="#">wrighty@gmail.com</a>
                            </td>
                            <td>
                                0181 321 456
                            </td>
                            <td>
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Jenny Combs
                            </td>
                            <td>
                                <a href="#">combsey@gmail.com</a>
                            </td>
                            <td>
                                0131 456 988
                            </td>
                            <td>
                                <a href="#" title="User"><img src="<%=Common.Util.Root%>locales/global/img/user.png" alt="User" width="16" height="16"></a><a href="#" title="Accept"><img src="<%=Common.Util.Root%>locales/global/img/accept.png" alt="Accept" width="16" height="16"></a><a href="#" title="Close"><img src="<%=Common.Util.Root%>locales/global/img/delete.png" alt="Close" width="16" height="16"></a>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br>
                <br>
            </div>
            <!--  end div #tab1 --><!--  END TAB 1 --><!--  START TAB 2 -->
            <div style="display: none;" id="tab-2">
                <h3>Forms</h3>
                <p>
                    Here are some example form elements. You can add some custom classes like small, medium and last to adjust to your own needs.
                </p>
                <form action="" method="get">
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
                        <textarea name="textarea" cols="50" rows="7" class="styled_textarea">Text area for lots of lines of text...</textarea>
                    </p>
                    <p>
                        <input class="button" value="Submit" type="button">
                    </p>
                </form>
            </div><!--  end div #tab-2 --><!--  END TAB 2 --><!--  START TAB 3 -->
            <div style="display: none;" id="tab-3">
                <h3>Typography</h3>
                <p>
                    Here
                    is an example of the typography used in this theme. I've listed the
                    different heading levels followed by some dummy paragraph text. I've
                    also sprinkled some links in too.
                </p>
                <h6>This is a H6 Heading</h6>
                <h5>This is a H5 Heading</h5>
                <h4>This is a H4 Heading</h4>
                <h3>This is a H3 Heading</h3>
                <h2>This is a H2 Heading</h2>
                <h1>This is a H1 Heading</h1>
                <p>
                    Sed
                    sed tellus neque, at porta felis. Pellentesque dignissim aliquam mi 
                    eget tristique. Aliquam erat volutpat. Lorem ipsum dolor sit amet, 
                    consectetur adipiscing elit. Cras sodales pretium pharetra. <a href="#">Vestibulum</a>
                    porta massa ut est sagittis fringilla. Curabitur sed posuere odio. Sed
                    ac nisi mi, a ultricies est. Nam elementum tristique purus, id viverra 
                    mi mattis eget. Integer in adipiscing dui. Sed lacinia porttitor 
                    turpis, ut accumsan nunc venenatis sed. Nunc ultricies <a href="#">imperdiet</a>
                    luctus. Suspendisse mollis, nisl a congue aliquet, turpis leo feugiat 
                    nulla, vitae fringilla eros neque eu risus. Mauris pellentesque 
                    placerat nisi ac pharetra. 
                </p>
            </div>
            <!--  end div #tab-3 -->
            <!--  END TAB 3 -->
        </div>
        <!--  END TABS -->
		<!-- The paginator -->
	        <ul class="paginator">
	        	<li><a href="#">Previous</a></li>
	            <li class="current"><a href="#">1</a></li>
	            <li><a href="#">2</a></li>
	            <li><a href="#">3</a></li>
	            <li><a href="#">4</a></li>
	            <li><a href="#">5</a></li>
	            <li><a href="#">6</a></li>
	            <li><a href="#">7</a></li>
	            <li><a href="#">8</a></li>
	            <li><a href="#">9</a></li>
	            <li><a href="#">Next</a></li>
	        </ul>
        <!-- Paginator end -->
    </div>
    <!--  end div #primary_content -->
</asp:content>
