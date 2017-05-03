
$(document).ready(function () {
    //DONT'T REMOVE THIS!!!
    //Bootstrap modal 'data-remote' correction:
	//source: http://stackoverflow.com/questions/12286332/twitter-bootstrap-remote-modal-shows-same-content-everytime
	$('body').on('hidden.bs.modal', '.modal', function () {
		$(this).removeData('bs.modal');
	});
});

$(document).ready(function () {
    // Initialise Globalize to the current UI culture (you need to have served up the relevant culture file for this to work)
    //Globalize.culture("pt-BR"); //alreray done in 'jquery.validate.globalize.js'
});

function showMessage(data) {
	if (data.Status === "0") {
		BootstrapDialog.show({
			type: BootstrapDialog.TYPE_WARNING, title: 'ATENÇÃO', message: data.Message, buttons: [{ label: 'OK', action: function (dialog) { dialog.close(); } }]
		});
	}
	else {
		BootstrapDialog.show({
			type: BootstrapDialog.TYPE_SUCCESS, title: 'SUCESSO', message: 'Ação realizada com sucesso!', buttons: [{
				label: 'OK',
				action: function (dialog) {
					dialog.close();
					
				}
			}]
		});
	}
}

function showSuccess() {
    BootstrapDialog.show({
        type: BootstrapDialog.TYPE_SUCCESS, title: 'SUCESSO', message: 'Ação realizada com sucesso!', buttons: [{
            label: 'OK',
            action: function (dialog) {
                dialog.close();
            }
        }]
    });
}

function showJsError(data, textStatus, jqXHR) {
    var msg = "";
    if (data.status === 500) {
        if (jqXHR !== undefined)
            msg = "<h3>" + jqXHR + "</h3>";
        var responseText = data.responseText.replace(/<br>/g, '');
        msg += "<div style='width: 550px; height:400px; overflow-y: scroll;'>" + responseText + "</div>";
    } else if (data.status === 401) {
        msg = "Acesso negado";
    }
    else {
        msg = data.responseText;
    }
	BootstrapDialog.show({
        type: BootstrapDialog.TYPE_DANGER, title: 'ERRO', cssClass: 'bs-example-modal-lg', message: msg,
            buttons: [{ label: 'OK', action: function (dialog) { dialog.close(); } }]
	});
}
