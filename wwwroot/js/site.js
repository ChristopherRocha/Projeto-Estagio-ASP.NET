function menuShow() {
    let menuMobile = document.querySelector('.mobile-menu')
    if (menuMobile.classList.contains('open')) {
        menuMobile.classList.remove('open');
        document.querySelector('.icon').src = "/img/menu_white_36dp.svg"

    }
    else {
        menuMobile.classList.add('open');
        document.querySelector('.icon').src = "/img/close_white_36dp.svg"
    }
}

function ajustarEndereco() {
    let estado = document.getElementById('estado').value
    let cidade = document.getElementById('cidade').value
    let bairro = document.getElementById('bairro').value;
    let rua = document.getElementById('rua').value;
    let numero = document.getElementById('numero').value;
    let complemento = document.getElementById('complemento').value;

    let endereco = estado+", "+cidade+", "+bairro + ", " + rua + ", " + numero + ", " + complemento;
    

    document.getElementById('tmp').value = endereco;

    

    return true;
}





cep.addEventListener('focusout', async() => {
    try {
        let cep = document.querySelector('#cep');
        let estado = document.getElementById('estado');
        let cidade = document.getElementById('cidade');
        let bairro = document.getElementById('bairro');
        let rua = document.getElementById('rua');
        let numero = document.getElementById('numero');

        const api = await fetch(`https://viacep.com.br/ws/${cep.value}/json/`);

        if (!api.ok) {
            throw { cep_error: 'ERRO 111111111' };
            
        }

        const apiCep = await api.json();

        estado.value = apiCep.estado;
        cidade.value = apiCep.localidade;
        bairro.value = apiCep.bairro;
        rua.value = apiCep.logradouro;

        if (rua.value == "undefined") {
            alert("Cep Inválido!");
        }


    }
    catch (error) {
        if (error?.cep_error) {
            console.log(error.cep_error);
        }
        else
            console.log('ERRO 22222222', error);
    }
});
