var ViewModel = function () {
    var self = this;
    self.numbers = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();

    self.getNumbers = function (item) {
        var boolActive = item.Active;

        self.IsActive = ko.computed(function () {
            return boolActive ? 'YES' : 'NO';            
        });


        ajaxHelper(numbersUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    var numbersUri = '/api/contactnumbers/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllPersons() {
        ajaxHelper(numbersUri, 'GET').done(function (data) {
            self.numbers(data);
        });
    }

    self.contacts = ko.observableArray();
    self.newNumber = {
        ContactPerson: ko.observable(),
        Number: ko.observable(),
        Active: false
    }

    var contactsUri = '/api/contactpersons/';

    function getContacts() {
        ajaxHelper(contactsUri, 'GET').done(function (data) {
            self.contacts(data);
        });
    }

    self.addNumber = function (formElement) {
        var contactNumber = {
            ContactPersonId: self.newNumber.ContactPerson().Id,
            Number: self.newNumber.Number(),
            Active: false
        };

        ajaxHelper(numbersUri, 'POST', contactNumber).done(function (item) {
            self.numbers.push(item);
        });
        
    }

    self.activateNumber = function (formElement) {
        var status = {
            Number: self.detail().Number,
            ContactPersonId: ko.observable(),
            Active: true
        };


        ajaxHelper(numbersUri, 'POST', status).done(function (item) {
            self.numbers.push(item);
        });

        ajaxHelper(numbersUri + self.detail().Id, 'DELETE', null).done(function (item) {
            self.numbers.push(item);
        });

    }

    getContacts();
    // Fetch the initial data.
    getAllPersons();
};

ko.applyBindings(new ViewModel());