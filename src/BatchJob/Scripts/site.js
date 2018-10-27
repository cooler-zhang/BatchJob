function deleteConfirm(hidName, id) {
    $.messager.confirm("删除确认!", "确定删除数据吗？", function () {
        $('#' + hidName).val(id);
        $('#formDelete').submit();
    });
}

function resolveService() {
    var service = $('#txtServiceAddress').val();
    if (service.length > 0) {
        $('#ddlContractMethod').empty();
        $.post('/home/resolve-service', { serviceAddress: service },
          function (data, status) {
              if ('success' == status) {
                  $.each(data.ContractInfos, function (idx, obj) {
                      var html = "<optgroup label='" + obj.ContractName + "'>";
                      $.each(obj.OperationInfos, function (idx2, obj2) {
                          html += "<option value='" + obj.ContractName + "_" + obj2.OperationName + "' parameter='" + JSON.stringify(obj2.ParameterInfos) + "'>" + obj2.OperationName + "</option>";
                      })
                      html += "</optgroup>"
                      $('#ddlContractMethod').append(html);
                  });
              }
          }, 'json');
    }
}

function contractMethodChanged2() {
    $('#divServiceParameters').empty();
    var parameters = $("#ddlContractMethod").find("option:selected").attr('parameter');
    $('#divServiceParameters').append("<input type='hidden' name='MethodParameter' value='" + parameters + "'>");
    $.each($.parseJSON(parameters), function (idx, obj) {
        $('#divServiceParameters').append("<label>" + obj.ParameterName + " : </label>" +
            "<input type='text' class='form-control text-box single-line' name='" + obj.ParameterName + "' value=''>");
    })
}