<%@ LANGUAGE = VBScript %>

<!--#INCLUDE FILE="pe_global_edit.asp" -->

<%
call GetInputText("Dispatcher_URL", 0, bufsize_medium)
call GetInputText("IsBatch", 0, bufsize_medium)
call GetInputText("ChannelAdapterExtension", 0, bufsize_medium)
%>

<!--#INCLUDE FILE="pe_post_footer.asp" -->
