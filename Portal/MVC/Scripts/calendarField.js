function CalendarField(label, today) {
    var self = this;
    var FIELD_CLASS_NAME = "calculator-field";
    var FIELD_CURRENT_ID = "today";

    self.Label = label;

    self._day = 1;
    self._month = 0;
    self._year = 2000;

    self._isCurrentDay = today || false;

    self.Init = function (day, month, year) {
        self._day = label;
        self._month = month;
        self._year = year;
    };


    self.SetCurrentDay = function () {
        self._isCurrentDay = true;
    };
    self.ClearCurrentDay = function () {
        self._isCurrentDay = false;
    };

    self.GetDiv = function () {
        var div = document.createElement("div");

        div.className = FIELD_CLASS_NAME; // + " " + 'mdl-cell mdl-cell--2-col';
        if (self._isCurrentDay)
            div.id = FIELD_CURRENT_ID;
        div.innerHTML = self.Label;
        div.click(function () {
            alert("MRK");
        });
        //div.setAttribute("data-target", ".bs-example-modal-lg");
        //div.setAttribute("data-toggle", "modal");
        //div.addClass('mdl-cell mdl-cell--1-col');

        div.setAttribute("data-label", self.Label);
        return div;
    };

    self.Draw = function () {
        var html = self.GetDiv();
        var container = document.getElementById("calendar-wrapper");
        container.appendChild(html);
    };
}
