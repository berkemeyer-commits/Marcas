<!--
/**
 * Funciones javascript que realizan la validación de
 * los campos de formularios
 * Autor : Marcos Báez
 */
 

/*
 * Verifica un campo requerido
 * param controlId : Id del Control
 */
function isValidRequired(controlId){
	var control = document.getElementById(controlId);	
	if (control == null) 
	    return false;
	if (control.type == 'checkbox'){
		return control.checked;
	}
	if (control.value == ''){
		return false;
	}
	return true;
} 	


messageObj = new DHTML_modalMessage();	// We only create one object of this class
messageObj.setShadowOffset(5);	// Large shadow


function displayMessage(url)
{
	
	messageObj.setSource(url);
	messageObj.setCssClassMessageBox(false);
	messageObj.setSize(400,200);
	messageObj.setShadowDivVisible(true);	// Enable shadow for these boxes
	messageObj.display();
}

function displayStaticMessage(messageContent,cssClass)
{
	messageObj.setHtmlContent(messageContent);
	messageObj.setSize(300,150);
	messageObj.setCssClassMessageBox(cssClass);
	messageObj.setSource(false);	// no html source since we want to use a static message here.
	messageObj.setShadowDivVisible(false);	// Disable shadow for these boxes	
	messageObj.display();
	
	
}

function closeMessage()
{
	messageObj.close();	
}
				
 -->