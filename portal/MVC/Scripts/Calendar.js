function daysInMonth(year, month) {
    return new Date(year, month, 0).getDate();
}

function getMonth(month) {
    switch (month) {
        case 0: return "Januar";
        case 1: return "Februar";
        case 2: return "Mart";
        case 3: return "April";
        case 4: return "Maj";
        case 5: return "Jun";
        case 6: return "Jul";
        case 7: return "Avgust";
        case 8: return "Septembar";
        case 9: return "Oktobar";
        case 10: return "Novembar";
        case 11: return "Decembar";
    }
}

function Calendar(wrapperId) {
    var self = this;
    var POPUP = document.getElementById("calendar-popup");
    var CALENDAR_TABLE_CLASS_NAME = "calendar-table";
    var days = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

    self._currentDay = (new Date()).getDate();
    self._currentYear = (new Date()).getFullYear();
    self._currentMonth = (new Date()).getMonth();
    self.Wrapper = document.getElementById(wrapperId);
    self.CalederFields = new Array(6 * 7);

    self.Init = function () {
        for (var i = 0; i < 6 * 7; i++) self.CalederFields[i] = new CalendarField(0, false);
        self._updateCalenderFields();

        self.Draw("calendar-wrapper");
        $("#prev").bind("click", function () { self.switchMonth(false); });
        $("#next").bind("click", function () { self.switchMonth(true); });

        $(".calculator-field").each(function () {
            $(this).bind("click", function () {
                //alert(this.dataset.label);
                POPUP.innerHTML = this.dataset.label;
            });
        });

    };

    self._updateCalenderFields = function () {
        var lastDay = daysInMonth(self._currentYear, self._currentMonth);
        var firstDay = 0;
        var firstDayString = (new Date(self._currentYear, self._currentMonth, firstDay + 1)).toDateString().split(" ")[0];
        var printNull = true;
        var today = new Date().getDate();
        for (var i = 0, index = 0; i < 6; i++) {
            for (var j in days) {
                if (today === firstDay + 1)
                    self.CalederFields[index].SetCurrentDay();
                if ((printNull && firstDayString !== days[j]) || (firstDay > lastDay)) {
                    self.CalederFields[index].Label = 0;
                    self.CalederFields[index].Init(firstDay, self._currentMonth, self._currentYear);
                } else {
                    printNull = false;
                    self.CalederFields[index].Label = ++firstDay;
                }
                index++;
            }
        }
    };
    self.Draw = function () {
        self.Wrapper.innerHTML = self.GetDiv().innerHTML;
        document.getElementById("label").innerHTML = getMonth(self._currentMonth) + " " + self._currentYear;
    };

    self.GetDiv = function () {
        var table = document.createElement("table");
        table.className = CALENDAR_TABLE_CLASS_NAME;

        var thead = document.createElement("thead");
        var theadTr = document.createElement("tr");
        for (var i in days) {
            var th = document.createElement("th");
            th.innerHTML = days[i];
            theadTr.appendChild(th);
        }
        thead.appendChild(theadTr);

        table.appendChild(thead);

        var tbody = document.createElement("tbody");

        var index = 0;
        for (i = 0; i < 6; i++) {
            var tr = document.createElement("tr");
            for (var j = 0; j < 7; j++) {
                var td = document.createElement("td");
                td.appendChild(self.CalederFields[index++].GetDiv());
                tr.appendChild(td);
            }
            tbody.appendChild(tr);
        }
        table.appendChild(tbody);
        var tableWrapper = document.createElement("div");
        tableWrapper.appendChild(table);
        return tableWrapper;
    };
    self.switchMonth = function (next) {
        if (next) {
            if (self._currentMonth === 11)
                self._currentYear++;
            self._currentMonth = (self._currentMonth + 1) % 12;
        } else {
            if (self._currentMonth === 0) {
                self._currentYear--;
                self._currentMonth = 11;
            } else {
                self._currentMonth--;
            }
        }
        self.Draw();
    }
}



