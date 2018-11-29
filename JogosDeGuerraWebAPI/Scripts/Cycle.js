/*
    cycle.js
    2018-05-15
    Public Domain.
    NO WARRANTY EXPRESSED OR IMPLIED. USE AT YOUR OWN RISK.
    This code should be minified before deployment.
    See http://javascript.crockford.com/jsmin.html
    USE YOUR OWN COPY. IT IS EXTREMELY UNWISE TO LOAD CODE FROM SERVERS YOU DO
    NOT CONTROL.
*/

// The file uses the WeakMap feature of ES6.

/*jslint eval */

/*property
    $ref, decycle, forEach, get, indexOf, isArray, keys, length, push,
    retrocycle, set, stringify, test
*/
if (typeof JSON.retrocycle !== 'function') {
    JSON.retrocycle = function retrocycle(o) {
        //debugger;

        var self = this;
        self.identifiers = [];
        self.refs = [];

        self.buildIdentifiers = function (value) {
            //debugger;

            if (!value || typeof value !== 'object') {
                return;
            }

            var item;

            if (Object.prototype.toString.apply(value) === '[object Array]') {
                for (var i = 0; i < value.length; i += 1) {
                    item = value[i];
                    if (!item || !item.$id || isNaN(item.$id)) {
                        if (item) {
                            self.buildIdentifiers(item);
                        }

                        continue;
                    }

                    self.identifiers[parseInt(item.$id)] = item;
                    self.buildIdentifiers(item);
                }

                return;
            }

            for (var name in value) {
                if (typeof value[name] !== 'object') {
                    continue;
                }

                item = value[name];

                if (!item || !item.$id || isNaN(item.$id)) {
                    if (item) {
                        self.buildIdentifiers(item);
                    }

                    continue;
                }

                self.identifiers[parseInt(item.$id)] = item;
                self.buildIdentifiers(item);
            }
        };

        self.rez = function (value) {

            // The rez function walks recursively through the object looking for $ref
            // properties. When it finds one that has a value that is a path, then it
            // replaces the $ref object with a reference to the value that is found by
            // the path.

            var i, item, name, path;

            if (value && typeof value === 'object') {
                if (Object.prototype.toString.apply(value) === '[object Array]') {
                    for (i = 0; i < value.length; i += 1) {
                        item = value[i];
                        if (item && typeof item === 'object') {

                            if (item.$ref)
                                path = item.$ref;

                            if (typeof path === 'string' && path != null) {
                                //self.refs[parseInt(path)] = {};

                                value[i] = self.identifiers[parseInt(path)];
                                continue;
                            }

                            //self.identifiers[parseInt(item.$id)] = item;
                            self.rez(item);
                        }
                    }
                } else {
                    for (name in value) {
                        if (typeof value[name] === 'object') {
                            item = value[name];
                            if (item) {
                                path = item.$ref;
                                if (typeof path === 'string' && path != null) {
                                    //self.refs[parseInt(path)] = {};

                                    value[name] = self.identifiers[parseInt(path)];
                                    continue;
                                }

                                //self.identifiers[parseInt(item.$id)] = item;
                                self.rez(item);
                            }
                        }
                    }
                }
            }

        };

        self.buildIdentifiers(o);
        self.rez(o);
        self.identifiers = []; // Clears the array
    };
}