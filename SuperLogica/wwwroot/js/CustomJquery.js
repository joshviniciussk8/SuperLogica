
$(document).ready(function () {
    var cep = $("#txtCEP");
    cep.mask('00000-000', { reverse: true });
    var cel = $("#celular");
    cel.mask('00 00000-0000', { reverse: true });
    var numero = $("#Numero");
    numero.mask('000000', { reverse: true });
});


$(document).ready(function () {
    $('#txtCEP').on('input', function () {
        var valorDoInput = $(this).val();

        if (valorDoInput.length > 7) {
            // Fazer uma chamada AJAX para a sua controller
            $.ajax({
                url: '/Contato/CarregaEndereco',
                method: 'POST',
                data: { cep: valorDoInput },
                success: function (resultado) {
                    if (resultado.cep == null) {
                        alert("CEP não encontrado");
                        return;
                    }
                    $('#Bairro').val(resultado.bairro);
                    $('#Logradouro').val(resultado.logradouro);
                    $('#Cidade').val(resultado.localidade);
                    $('#Estado').val(resultado.uf);
                },
                error: function (erro) {
                    console.error('Erro na requisição AJAX: ', erro);
                }
            });
        }
    });
});

$(document).ready(function () {
    $('#seuFormulario').submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: '/Contato/CriarContato',
            method: 'POST',
            data: $(this).serialize(),
            success: function (resultado) {
                if (resultado.sucesso) {
                    // A operação foi bem-sucedida
                    // Faça o que for necessário
                    alert(resultado.mensagem);
                } else {
                    // Exibir alerta em caso de erro
                    alert('Erro: ' + resultado.mensagem);
                }
            },
            error: function () {
                alert('Erro na requisição AJAX.');
            }
        });
    });
});


//<script>
//    $(document).ready(function () {
//        $('#meuBotao').click(function () {
//            $.ajax({
//                url: '@Url.Action("EditarContato", "Contato")',
//                type: 'Post', // ou 'POST' conforme necessário
//                data: { cep: valorDoInput },
//                success: function (data) {
//                    // Faça algo com os dados recebidos, se necessário
//                },
//                error: function () {
//                    alert('Erro na requisição Ajax.');
//                }
//            });
//        });
//    });
//</script>