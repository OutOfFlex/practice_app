/**
 * Отправляет ajax запрос
 * @param {string} url конечный адрес для запроса
 * @param {object} data данные передаваемые в запросе
 * @param {string} method тип запроса (get, post, ...)
 */
function sendRequest(url, data, method) {
    return $.ajax({
        url: url,
        data: data,
        type: method,
        cache: false,
        error: function (xmlHttpRequest, errorText, thrownError) {
            console.log(errorText + "_" + thrownError);
        },
    });
}

function loadPartialTo(url, destination, reqData) {
    return sendRequest(url, reqData, "get")
        .done(function (result) {
            if (typeof (destination) == 'string')
                $('#' + destination).html(result);
            else
                destination.html(result);
        });
}

/**
 * 
 * @param {{ key: string, value: string}[]} inputsWithErrors
 */
function markInputsAsInvalid(inputsWithErrors) {
    inputsWithErrors.forEach(function (o) {
        const $input = $('#' + o.key);

        $input.addClass('is-invalid');

        let $invalidText = $input.siblings('.invalid-feedback');
        if ($invalidText.length == 0) {
            $invalidText = $('<div class="invalid-feedback"></div>');
            $input.after($invalidText);
        }
        $invalidText.html(o.value);
    });
}

/**
 * Отправляет запрос к api методу
 * @param {string} url
 * @param {object} reqData отправляемые в запросе данные
 * @param {string} method get, post, put, ...
 * @param {boolean} blockRedirect
 */
function sendAction(url, reqData, method, blockRedirect) {
    return sendRequest(url, reqData, method)
        .done(function (result) {
            if (result.message)
                alert(result.message);

            if (result.isSuccess) {
                let href = result.href || window.location.href;

                !blockRedirect && window.location.assign(href);
            }
            else if (result.invalidInputs)
                markInputsAsInvalid(result.invalidInputs);
            else if (result.message)
                alert("Произошла ошибка")
        })
        .fail(function () {
            alert("Произошла ошибка");
        });
}

/**
 * Обрабатывает отправку формы
 * @param {Event} e
 */
function onFormSubmit(e) {
    $(e.target).find('.is-invalid').removeClass('is-invalid');

    e.preventDefault();
    sendAction(e.target.getAttribute('action'), $(e.target).serialize(), e.target.getAttribute('method'));
}