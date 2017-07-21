var Alert = new Object({
    alert: function (msg) {
        layer.open({
            content: msg
              , btn: '我知道了'
        });
    },
    confirm: function (msg, _callback) {
        layer.open({
            content: msg
              , btn: ['确定', '不要']
              , yes: function (index) {
                  //location.reload();
                  _callback();
                  layer.close(index);
              }
        });
    },
    loding: function (msg) {
        layer.open({
            type: 2
           , content: msg
        });
    },
    sureAlert: function (msg, _callback) {
        layer.open({
            content: msg,
            btn: '我知道了',
            shadeClose: false,
            yes: function () {
                _callback();
            }
        });
    }
});