$(document).ready(function () {
    Master.Init();
});


var Master = {
    _Callback: {},
    _OnReadyHandler: {},
    _HasError: {},


    Path:
        {
            Root: {},
            SharedFolder: {},
            PagesFolder: {},
            Client: {},
        },

    Messages:
    {
        DeleteItem: "مورد انتخاب شده حذف شود؟",
        SaveItem: "تغییرات  اعمال شده ذخیره گردد؟",
        DeleteItemForever: "امکان بازیابی موجود نمی باشد، مورد انتخاب شده حذف شود؟",
        DeleteItems: "موارد انتخاب شده حذف شوند؟",
        DeleteItemsForever: "امکان بازیابی موجود نمی باشد، موارد انتخاب شده حذف شوند؟",
        NoItemSelected: "موردی انتخاب نشده است.",
        Confirm: "موارد انتخاب شده مورد تایید است؟",
        Cancel: "موارد انتخاب شده لغو شوند؟",
        Type:
        {
            Info: "info",
            Error: "error",
            Success: "success",
            Warning: "warning",
            Question: "question"
        }
    },
    DXUtil:
        {
            ConfirmDelete: function (callback, forever) {
                Master.Confirm({ message: forever == true ? Master.Messages.DeleteItemForever : Master.Messages.DeleteItem, type: "warning", onConfirm: callback });
            },
            SaveDialog: function (callback) {
                //Master.SchedulerDialog({ title: "ذخیره سازی", onConfirm: callback, confirmText: "ذخیره", message: Master.Messages.SaveItem });
            },
            DeleteDialog: function (grid, callback) {
                if (grid.GetSelectedRowCount() < 1) {
                    Master.GetRoot().Alert({ message: Master.Messages.NoItemSelected, type: "warning" });
                    return;
                }
                //Master.SchedulerDialog({ title: "حذف", onConfirm: callback, confirmText: "حذف", message: Master.Messages.DeleteItems });
            },
            GridDeleteSelected: function (grid, callback, forever) {
                if (grid.GetSelectedRowCount() < 1) {
                    Master.GetRoot().Alert({ message: Master.Messages.NoItemSelected, type: "warning" });
                    return;
                }
                Master.GetRoot().Confirm({ message: forever == true ? Master.Messages.DeleteItemsForever : Master.Messages.DeleteItems, type: "warning", onConfirm: callback });
            },
            GridDeleteItem: function (grid, callback, forever) {
                if (grid.GetFocusedRowKey() == null) {
                    Master.GetRoot().Alert({ message: Master.Messages.NoItemSelected, type: "warning" });
                    return;
                }
                Master.GetRoot().Confirm({ message: forever == true ? Master.Messages.DeleteItemForever : Master.Messages.DeleteItem, type: "warning", onConfirm: callback });
            },
            GridOprationItem: function (grid, callback, message, type) {
                if (grid.GetFocusedRowKey() == null) {
                    Master.GetRoot().Alert({ message: Master.Messages.NoItemSelected, type: "warning" });
                    return;
                }
                if (message != null) {
                    Master.GetRoot().Confirm({ message: message, type: type, onConfirm: callback });
                }
                else {
                    callback();
                }
            },
            GridOprationItems: function (grid, callback, message, type) {
                if (grid.GetSelectedRowCount() < 1) {
                    Master.GetRoot().Alert({ message: Master.Messages.NoItemSelected, type: "warning" });
                    return;
                }
                if (message != null) {
                    Master.GetRoot().Confirm({ message: message, type: type, onConfirm: callback });
                }
                else {
                    callback();
                }
            },
            AjustGridSize: function (s, e) {
                var height = $(".main-content").height() - 40;
                s.SetHeight(height);
            },
            AjustGridSizePopup: function (s, e) {
                var height = $(".popup-window-content").height() - 52;
                s.SetHeight(height);
            },
            AjustTreeSize: function (s, e) {
                var height = $(".main-content").height() - 60;
                s.SetHeight(height);
            }
        },
    PageSizes:
        {
            Width:
                {
                    W0: 300,
                    W1: 450,
                    W2: 600,
                    W3: 900,
                    W9: 380
                },

            Height:
                {
                    H1: 200,
                    H2: 250,
                    H3: 300,
                    H4: 400,
                    H5: 550,
                    H6: 700,
                    H9: 390,
                    H10: 660
                }
        },

    GlobalEvents:
        {
            OnCallbackError: function (s, e) {
                _HasError = true;
                e.handled = true;
                setTimeout(function () {
                    Master.GetRoot().Alert({ message: e.message, type: "error", title: "بروز خطا" });
                }, 500);
                if ((e.control instanceof ASPxClientGridView) == true) {
                    e.handled = false;
                }
               
            },
            OnEndCallback: function (s, e) {
                try {
                    var result = $.parseJSON(s.cpResult);
                    s.cpResult = null;
                    //console.log(result);
                    var redirect = result.values.dxRedirect;
                    if (result != null && result.message != null) {
                        Master.GetRoot().Alert({
                            message: result.message.content, title: result.message.title, type: result.message.type,
                            onConfirm: function () {
                                if (redirect != null)
                                    window.top.location = result.values.dxRedirect;
                                if (_Callback != null && _HasError == false) {
                                    _Callback(result);
                                }
                            }
                        });
                    }
                    else if (_Callback != null && _HasError == false) {
                        if (redirect != null)
                            window.top.location = result.values.dxRedirect;
                        _Callback(result);
                    }
                    else {
                        if (redirect != null)
                            window.top.location = result.values.dxRedirect;
                    }
                    if (_OnReadyHandler != null)
                        _OnReadyHandler();
                } catch (e) {
                    //throw e;
                }

            },
            OnValidationCompleted: function (s, e) {
                Master.IsValid = e.isValid;
            }
        },

    Init: function () {
        //Master.FixKeyboard();
    },


    OnReady: function (handler) {
        _OnReadyHandler = handler;
    },

    Redirect: function (url) {
        setTimeout(function () {
            window.location = url;
        }, 100);
    },

    PerformCallback: function (action, args, callback) {
        var data = { action: action, args: args };
        MasterCallbackPanel.PerformCallback(JSON.stringify(data));
        _Callback = callback;
        _HasError = false;
    },

    Ajax: function (action, args, callback) {
        var data = { action: action, args: args };
        MasterCallback.PerformCallback(JSON.stringify(data));
        _Callback = callback;
        _HasError = false;
    },


    OpenDialogWindow: function (args) {
        var title = args["title"] == null ? "New Page" : args["title"];
        var url = new String(args["url"] == null ? "" : args["url"]);
        var data = args["data"];
        var width = args["width"] == null ? 500 : args["width"];
        if (args["width"] == "MAX")
            width = document.body.clientWidth - 10;
        var height = args["height"] == null ? 300 : args["height"];
        if (args["height"] == "MAX")
            height = document.body.clientHeight - 10;
        var opener = args["opener"] == null ? this : args["opener"];
        var onClose = args["onClose"] == null ? function () { } : args["onClose"];
        var id = args["id"];
        var maximized = args["maximized"] == null ? false : Boolean(args["maximized"]);

        var win = null;
        if (id == null) {
            var count = MasterPopupControl.GetWindowCount();
            for (i = 0; i < count; i++) {
                win = MasterPopupControl.GetWindow(i);
                if (win.title == title) return;
                if (!MasterPopupControl.IsWindowVisible(win))
                    break;
            }
        }
        else {
            win = MasterPopupControl.GetWindowByName(id);
        }
        //console.log(win);
        win.onClose = function (s, e) {
            MasterPopupControl.SetWindowContentUrl(e.window, "javascript:void(0)");
            e.window.title = null;
            e.window.dialogResult = false;
            e.window.CustomClosingHandler = null;
        }

        win.onClosing = function (s, e) {
            if (win.CustomClosingHandler != null)
                win.CustomClosingHandler(s, e);
            var obj = new Object();
            obj.dialogResult = e.window.dialogResult;
            obj.dialogParams = e.window.dialogParams;
            onClose(win, obj);
        }

        win.onShown = function (s, e) {
            var iframe = MasterPopupControl.GetWindowContentIFrame(win);
            var r = $('<iframe name="' + iframe.id + '" id="' + iframe.id + '" frameborder="0" scrolling="0" style="height:100%;width:100%;" class="dxpc-iFrame" />');
            $(iframe).replaceWith(r);
            var refreshBtn = document.getElementById("MasterPopupControl_HRB" + win.index);

            ASPxClientUtils.AttachEventToElement(refreshBtn, "click", function () {
                var form = new Master.PostIFrameWithData(win.url, $(r)[0], data);
                form.send();
                $(iframe).remove();
            });
            var form = new Master.PostIFrameWithData(win.url, $(r)[0], data);
            form.send();
            $(iframe).remove();
        }

        win.title = title;
        win.opener = opener;
        win.url = url;
        win.SetHeaderText(title);
        MasterPopupControl.SetWindowSize(win, width, height);
        MasterPopupControl.ShowWindow(win);
        MasterPopupControl.SetWindowContentUrl(win, "javascript:void(0)");
        //MasterPopupControl.SetWindowContentUrl(win, url);
        if (maximized == true)
            MasterPopupControl.SetWindowMaximized(win, true);
        else
            MasterPopupControl.SetWindowMaximized(win, false);
        Master.IsShowingPopup = true;
    },

    GetCurrentDialog: function () {
        var count = MasterPopupControl.GetWindowCount();

        for (i = count - 1; i >= 0; i--) {
            win = MasterPopupControl.GetWindow(i);
            if (MasterPopupControl.IsWindowVisible(win)) {
                return win;
                break;
            }
        }
        return null;
    },
    SetWindowSize: function (width, height) {
        var win = Master.GetCurrentDialog();
        MasterPopupControl.SetWindowSize(win, width, height);
        MasterPopupControl.UpdateWindowPosition(win);
    },

    Confirm: function (args) {
        var title = args["title"] == null ? "" : args["title"];
        var msg = args["message"] == null ? "" : args["message"];
        var onConfirm = args["onConfirm"] == null ? function () { } : args["onConfirm"];
        var onCancel = args["onCancel"] == null ? function () { } : args["onCancel"];
        var showCancel = args["showCancel"] == null ? true : args["showCancel"];
        var width = args["width"] == null ? 400 : args["width"];
        var height = args["height"] == null ? 80 : args["height"];
        var confirmText = args["confirmText"] == null ? "تائید" : args["confirmText"];
        var cancelText = args["cancelText"] == null ? "انصراف" : args["cancelText"];
        var btnWidth = args["buttonWidth"] == null ? 100 : args["buttonWidth"];
        var confirmWidth = args["confirmWidth"] == null ? btnWidth : args["confirmWidth"];
        var cancelWidth = args["cancelWidth"] == null ? btnWidth : args["cancelWidth"];

        //
        var type = args["type"] == null ? "info" : args["type"];

        var win = MasterPopupControl.GetWindowByName('pop');

        win.onClose = function (s, e) {
            MasterPopupControl.SetWindowContentUrl(e.window, "javascript:void(0)");
            e.window.title = null;
            e.window.dialogResult = false;
            e.window.CustomClosingHandler = null;
        }
        win.onClosing = function (s, e) {
            if (win.CustomClosingHandler != null)
                win.CustomClosingHandler(s, e);
            //onClose(s, e);
        }
        win.onShown = function (s, e) {
        }

        $("#MasterPopupControl_PWH" + win.index + " .dxpc-headerImg").removeClass("sprite_warning sprite_success sprite_error sprite_info").addClass("sprite_" + type);

        win.SetHeaderText(title);
        MasterPopupControl.SetWindowContentHTML(win, msg);
        MasterPopupControl.SetWindowSize(win, width, height);
        //
        btnPopupOk.GetMainElement().onclick = function () { onConfirm(); MasterPopupControl.HideWindow(win); };
        btnPopupOk.SetText(confirmText);
        btnPopupOk.SetWidth(confirmWidth);
        btnPopupCancel.GetMainElement().onclick = function () { onCancel(); MasterPopupControl.HideWindow(win); };
        btnPopupCancel.SetText(cancelText);
        btnPopupCancel.SetWidth(cancelWidth);
        //  
        btnPopupCancel.SetVisible(showCancel);
        //   
        MasterPopupControl.ShowWindow(win);
        Master.IsShowingPopup = true;
    },


    Alert: function (args) {
        args["showCancel"] = false;
        args["onCancel"] = null;
        Master.Confirm(args);
    },

    CloseActiveWindow: function (dialogResult, dialogParams) {
        var win = Master.GetCurrentDialog();
        win.dialogResult = dialogResult == null ? false : dialogResult;
        win.dialogParams = dialogParams == null ? new Object() : dialogParams;
        MasterPopupControl.HideWindow(win);
    },

    GetRoot: function () {
        var win = window;
        while (win != null) {
            newWin = win.parent != null ? win.parent : win.opener;
            if (newWin != win)
                win = newWin;
            else
                break;
        }
        if (win != null)
            return win.Master;
        return window.Master;
    },
    HandleEnter: function (e) {
        e = e.htmlEvent;
        if (e.keyCode === 13) {
            if (e.preventDefault)
                e.preventDefault();
            else
                e.returnValue = false;
        }
    },
    PostIFrameWithData: function (url, iframe, data) {

        var object = this;
        object.iframe = iframe;
        object.time = new Date().getTime();
        object.form = $('<form action="' + url + '" target="' + iframe.name + '" method="post"  id="form' + object.time + '"></form>');
        object.value = "";
        for (var i in data) {
            if (object.value == "") {
                object.value += "{";
            }
            object.value += "\"" + i + "\":" + "\"" + data[i] + "\",";
        }

        object.send = function () {
            object.value = object.value != "" ? object.value.substring(0, object.value.length - 1) + "}" : null;
            $("<input type='hidden' />").attr("name", "__REQUEST_DATA").attr("value", object.value).appendTo(object.form);
            $("body").append(object.form);
            object.form.submit();
            $(object.iframe).load(function () { $(object.form).remove(); });
        }

    },
    FixKeyboard: function () {
        $("input[type=text]").keypress(function (evt) {
            if (evt.which) {
                //alert(evt.which);
                var charStr = String.fromCharCode(evt.which);
                var transformedChar = charStr == "ی" ? "ي" : charStr;
                if (transformedChar != charStr) {
                    var start = this.selectionStart, end = this.selectionEnd, val = this.value;
                    this.value = val.slice(0, start) + transformedChar + val.slice(end);

                    // Move the caret
                    this.selectionStart = this.selectionEnd = start + 1;
                    return false;
                }
            }
        });
    }
};



