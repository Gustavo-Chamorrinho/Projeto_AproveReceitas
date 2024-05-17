function verificarRecaptcha(event) {
    
    event.preventDefault();

    var token = grecaptcha.getResponse();
    if (token.length === 0) {
        alert('Por favor, complete o reCAPTCHA.');
    } else {
        
        var data = { token: token };

        
        fetch('/Home/VerificarRecaptcha', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
            .then(response => response.json())
            .then(data => {
                
                if (data.success) {
                    
                    document.getElementById('login_form').submit(); 
                } else {
                    
                    alert('Token reCAPTCHA inválido.');
                }
            })
            .catch(error => {
                console.error('Erro ao verificar o token reCAPTCHA:', error);
                
            });
    }
}

document.getElementById('login_form').addEventListener('submit', verificarRecaptcha);