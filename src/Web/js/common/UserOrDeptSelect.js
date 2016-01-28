
function CloseHidenDiv() {
    var deletediv = document.getElementById("div_ZCDiv");
    var Mydiv = document.getElementById("DivSel");
    Mydiv.style.display = "none";
    deletediv.style.display = "none";
}



function getsubcompany(ControlID) {
    var Array = ControlID.split(",");
    if (Array.length == 2) {
        var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&Subflag=sub";
        var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
        if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
            var splitInfo = returnValue.split("|");
            window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = splitInfo[0].toString();
            window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = splitInfo[1].toString();
        }
        else if (returnValue == "ClearInfo") {
            window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "";
            window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "";
        }
    }
    else {
        var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&Subflag=sub";
        var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
        if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
            var ID = "";
            var Name = "";
            var getinfo = returnValue.split(",");
            for (var i = 0; i < getinfo.length; i++) {
                var c = getinfo[i].toString();
                ID += c.substring(0, c.indexOf("|")) + ",";
                Name += c.substring(c.indexOf("|") + 1, c.length) + ",";
            }
            ID = ID.substring(0, ID.length - 1);
            Name = Name.substring(0, Name.length - 1);
            if (window.parent.window.frames["salaryPage"][0].document.getElementById(Array[0]).value != "") {
                var Oldvalue = window.parent.window.frames["salaryPage"][0].document.getElementById(Array[0]).value;
                var Newvalue = Name;
                var Tempvalue = "";
                if (Newvalue.indexOf(Oldvalue) >= 0) {
                    var Splitinfo = Newvalue.split(',');
                    for (var i = 0; i < Splitinfo.length; i++) {
                        if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                            Tempvalue += Splitinfo[i].toString() + ",";
                        }
                    }
                    Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                    if (Tempvalue.length > 0) {

                        window.parent.window.frames["salaryPage"][0].document.getElementById(Array[0]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "," + Tempvalue;
                    }
                }
                else {
                    Oldvalue = Oldvalue.replace(/,/g, "");
                    var Splitinfo = Newvalue.split(',');
                    for (var i = 0; i < Splitinfo.length; i++) {
                        if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                            Tempvalue += Splitinfo[i].toString() + ",";
                        }
                    }
                    Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                    if (Tempvalue.length > 0) {
                        window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "," + Tempvalue;
                    }
                }
            }
            else {
                window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = Name;
            }
            if (window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value != "") {
                var Oldvalue = window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value;
                var Newvalue = ID;
                var Tempvalue = "";
                if (Newvalue.indexOf(Oldvalue) >= 0) {
                    var Splitinfo = Newvalue.split(',');
                    for (var i = 0; i < Splitinfo.length; i++) {
                        if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                            Tempvalue += Splitinfo[i].toString() + ",";
                        }
                    }
                    Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                    if (Tempvalue.length > 0) {
                        window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "," + Tempvalue;
                    }
                }
                else {
                    Oldvalue = Oldvalue.replace(/,/g, "");
                    var Splitinfo = Newvalue.split(',');
                    for (var i = 0; i < Splitinfo.length; i++) {
                        if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                            Tempvalue += Splitinfo[i].toString() + ",";
                        }
                    }
                    Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                    if (Tempvalue.length > 0) {
                        window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value += window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "," + Tempvalue;
                    }
                }
            }
            else {
                window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = ID;
            }
        }
        else if (returnValue == "ClearInfo") {
            window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value = "";
            window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value = "";
        }
    }

}

function SelectUserOrDept(ControlID) {
    var Array = ControlID.split(",");
    if (Array[0].indexOf("Dept") >= 0) {
        if (Array.length == 3) {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=1";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var splitInfo = returnValue.split("|");
                window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = splitInfo[0].toString();
                window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = splitInfo[1].toString();
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = "";
                window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = "";
            }
        }
        else {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=2";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var ID = "";
                var Name = "";
                var getinfo = returnValue.split(",");
                for (var i = 0; i < getinfo.length; i++) {
                    var c = getinfo[i].toString();
                    ID += c.substring(0, c.indexOf("|")) + ",";
                    Name += c.substring(c.indexOf("|") + 1, c.length) + ",";
                }
                ID = ID.substring(0, ID.length - 1);
                Name = Name.substring(0, Name.length - 1);
                //  window.parent.document.getElementById(Array[1]).value =ID;
                if (window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value != "") {
                    var Oldvalue = window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value;
                    var Newvalue = Name;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {

                            window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value += window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                    else {
                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value += window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                    // window.parent.document.getElementById(Array[0]).value+=window.parent.document.getElementById(Array[0]).value =","+Name;
                }
                else {
                    window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = Name;
                }
                if (window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value != "") {
                    // window.parent.window.frames["Main"].document.getElementById(Array[1]).value+=  window.parent.window.frames["Main"].document.getElementById(Array[1]).value =","+ID;
                    var Oldvalue = window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value;
                    var Newvalue = ID;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value += window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                    else {
                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value += window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                }
                else {
                    window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = ID;
                }
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = "";
                window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = "";
            }
        }
    }
    if (Array[0].indexOf("User") >= 0) {

        if (Array.length == 2) {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var splitInfo = returnValue.split("|");


                window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = splitInfo[0].toString();
                window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = splitInfo[1].toString();
                // window.parent.document.getElementById(Array[1]).value =
                //window.parent.document.getElementById(Array[0]).value =splitInfo[1].toString();
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = "";
                window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = "";
                //window.parent.document.getElementById(Array[0]).value="";
                //window.parent.document.getElementById(Array[1]).value=""; 
            }
        }
        else {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=2";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var ID = "";
                var Name = "";
                var getinfo = returnValue.split(",");
                for (var i = 0; i < getinfo.length; i++) {
                    var c = getinfo[i].toString();
                    ID += c.substring(0, c.indexOf("|")) + ",";
                    Name += c.substring(c.indexOf("|") + 1, c.length) + ",";
                }
                ID = ID.substring(0, ID.length - 1);
                Name = Name.substring(0, Name.length - 1);
                //window.parent.document.getElementById(Array[1]).value =ID; 
                if (window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value != "") {
                    var Oldvalue = window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value;
                    var Newvalue = Name;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value += window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                    else {

                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value += window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                }
                else {
                    window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = Name;
                }
                if (window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value != "") {
                    var Oldvalue = window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value;
                    var Newvalue = ID;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value += window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                    else {
                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value += window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                }
                else {
                    window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = ID;
                }
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value = "";
                window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value = "";
            }
        }
    }

}



function alertdiv(ControlID) {
    var Array = ControlID.split(",");
    if (Array[0].indexOf("Dept") >= 0) {
        if (Array.length == 2) {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=1";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var splitInfo = returnValue.split("|");
                window.parent.window.frames["Main"].document.getElementById(Array[1]).value = splitInfo[0].toString();
                window.parent.window.frames["Main"].document.getElementById(Array[0]).value = splitInfo[1].toString();
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames["Main"].document.getElementById(Array[0]).value = "";
                window.parent.window.frames["Main"].document.getElementById(Array[1]).value = "";
            }
        }
        else {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=2";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var ID = "";
                var Name = "";
                
                var getinfo = returnValue.split(",");
                for (var i = 0; i < getinfo.length; i++) {
                    var c = getinfo[i].toString();
                    ID += c.substring(0, c.indexOf("|")) + ",";
                    Name += c.substring(c.indexOf("|") + 1, c.length) + ",";
                }
                ID = ID.substring(0, ID.length - 1);
                Name = Name.substring(0, Name.length - 1);
                //  window.parent.document.getElementById(Array[1]).value =ID;
                if (window.parent.window.frames["Main"].document.getElementById(Array[0]).value != "") {
                    var Oldvalue = window.parent.window.frames["Main"].document.getElementById(Array[0]).value;
                    var Newvalue = Name;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {

                            window.parent.window.frames["Main"].document.getElementById(Array[0]).value += window.parent.window.frames["Main"].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                    else {
                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["Main"].document.getElementById(Array[0]).value += window.parent.window.frames["Main"].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                    // window.parent.document.getElementById(Array[0]).value+=window.parent.document.getElementById(Array[0]).value =","+Name;
                }
                else {
                    window.parent.window.frames["Main"].document.getElementById(Array[0]).value = Name;
                }
                if (window.parent.window.frames["Main"].document.getElementById(Array[1]).value != "") {
                    // window.parent.window.frames["Main"].document.getElementById(Array[1]).value+=  window.parent.window.frames["Main"].document.getElementById(Array[1]).value =","+ID;
                    var Oldvalue = window.parent.window.frames["Main"].document.getElementById(Array[1]).value;
                    var Newvalue = ID;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["Main"].document.getElementById(Array[1]).value += window.parent.window.frames["Main"].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                    else {
                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["Main"].document.getElementById(Array[1]).value += window.parent.window.frames["Main"].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                }
                else {
                    window.parent.window.frames["Main"].document.getElementById(Array[1]).value = ID;
                }
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames["Main"].document.getElementById(Array[0]).value = "";
                window.parent.window.frames["Main"].document.getElementById(Array[1]).value = "";
            }
        }
    }
    if (Array[0].indexOf("User") >= 0) {

        if (Array.length == 2) {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var splitInfo = returnValue.split("|");


                window.parent.window.frames["Main"].document.getElementById(Array[1]).value = splitInfo[0].toString();
                window.parent.window.frames["Main"].document.getElementById(Array[0]).value = splitInfo[1].toString();
                // window.parent.document.getElementById(Array[1]).value =
                //window.parent.document.getElementById(Array[0]).value =splitInfo[1].toString();
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames["Main"].document.getElementById(Array[1]).value = "";
                window.parent.window.frames["Main"].document.getElementById(Array[0]).value = "";
                //window.parent.document.getElementById(Array[0]).value="";
                //window.parent.document.getElementById(Array[1]).value=""; 
            }
        }
        else {
            var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=2";
            var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
            if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                var ID = "";
                var Name = "";
                var getinfo = returnValue.split(",");
                for (var i = 0; i < getinfo.length; i++) {
                    var c = getinfo[i].toString();
                    ID += c.substring(0, c.indexOf("|")) + ",";
                    Name += c.substring(c.indexOf("|") + 1, c.length) + ",";
                }
                ID = ID.substring(0, ID.length - 1);
                Name = Name.substring(0, Name.length - 1);
                //window.parent.document.getElementById(Array[1]).value =ID; 
                if (window.parent.window.frames["Main"].document.getElementById(Array[0]).value != "") {
                    var Oldvalue = window.parent.window.frames["Main"].document.getElementById(Array[0]).value;
                    var Newvalue = Name;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["Main"].document.getElementById(Array[0]).value += window.parent.window.frames["Main"].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                    else {

                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["Main"].document.getElementById(Array[0]).value += window.parent.window.frames["Main"].document.getElementById(Array[0]).value = "," + Tempvalue;
                        }
                    }
                }
                else {
                    window.parent.window.frames["Main"].document.getElementById(Array[0]).value = Name;
                }
                if (window.parent.window.frames["Main"].document.getElementById(Array[1]).value != "") {
                    var Oldvalue = window.parent.window.frames["Main"].document.getElementById(Array[1]).value;
                    var Newvalue = ID;
                    var Tempvalue = "";
                    if (Newvalue.indexOf(Oldvalue) >= 0) {
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["Main"].document.getElementById(Array[1]).value += window.parent.window.frames["Main"].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                    else {
                        Oldvalue = Oldvalue.replace(/,/g, "");
                        var Splitinfo = Newvalue.split(',');
                        for (var i = 0; i < Splitinfo.length; i++) {
                            if (Oldvalue.indexOf(Splitinfo[i].toString()) < 0) {
                                Tempvalue += Splitinfo[i].toString() + ",";
                            }
                        }
                        Tempvalue = Tempvalue.substring(0, Tempvalue.length - 1);
                        if (Tempvalue.length > 0) {
                            window.parent.window.frames["Main"].document.getElementById(Array[1]).value += window.parent.window.frames["Main"].document.getElementById(Array[1]).value = "," + Tempvalue;
                        }
                    }
                }
                else {
                    window.parent.window.frames["Main"].document.getElementById(Array[1]).value = ID;
                }
            }
            else if (returnValue == "ClearInfo") {
                window.parent.window.frames["Main"].document.getElementById(Array[0]).value = "";
                window.parent.window.frames["Main"].document.getElementById(Array[1]).value = "";
            }
        }
    }

}

//输出遮罩层
function alertHidenDiv() {
    /**第一步：创建DIV遮罩层。*/
    var sWidth, sHeight;
    sWidth = window.screen.availWidth;
    if (window.screen.availHeight > document.body.scrollHeight) {  //当高度少于一屏
        sHeight = window.screen.availHeight;
    } else {//当高度大于一屏
        sHeight = document.body.scrollHeight;
    }
    var str = "";

    str = "<div id='div_ZCDiv'  style='position:absolute;top:0;left:0;background:#777;filter:Alpha(opacity=30);opacity:0.3;height:" + sHeight + ";width:" + sWidth + ";zIndex:900;' >";
    str += "<iframe id='div_ZCDiv_aaaa' style='position: absolute; z-index:-1; width:" + sWidth + "; height:" + sHeight + ";' frameborder='0'>  </iframe>";
    //  str+="<div  style='position:absolute; top:0; left:0; width:"+sWidth+"; height:"+sHeight+"; background:#FDF3D9; border:1px solid #EEAC53'></div>";
    //str+="<iframe src='javascript:false' style='Z-INDEX:-1; FILTER:progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0); LEFT:0px; VISIBILITY:inherit; WIDTH:"+sWidth+"; POSITION:absolute; TOP:0px; HEIGHT:"+sHeight+"'>";
    str += "</div>";
    insertHtml("afterBegin", document.body, str);
}






//--关闭div
function hide() {
    document.getElementById("DivSel").style.display = "none";
}