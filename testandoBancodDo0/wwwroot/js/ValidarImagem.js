document.getElementById('cad_receitas').addEventListener('submit', function (event) {
    var fileInput = document.getElementById('imagem');
    var fileName = fileInput.value;
    var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
    var allowedExts = ['jpg', 'jpeg', 'png', 'gif'];
    var maxSizeMB = 5; //arquivos até 5mb


    if (fileName.trim() === '') {
        alert('Por favor, selecione uma imagem.');
        event.preventDefault();
        return false;
    }


    if (allowedExts.indexOf(ext) === -1) {
        alert('Somente arquivos com as seguintes extensões são permitidos: ' + allowedExts.join(', '));
        event.preventDefault();
        return false;
    }

    //tamanho do arquivo selecionado
    if (fileInput.files[0].size / (1024 * 1024) > maxSizeMB) {
        alert('O tamanho máximo do arquivo é de ' + maxSizeMB + ' MB.');
        event.preventDefault();
        return false;
    }

    return true;
});