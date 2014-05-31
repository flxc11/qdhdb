/** WebStorm v0.1.0
 * @author: Merainy
 * @date: 13-11-22
 * @email: Merainy.a@Gmail.com
 * @description:
 * ========================
 */
(function() {
    $(function() {
        var columns, current, easing, grid, hideItem, showItem, time,
            _this = this;
        grid = $(".members");
        current = {
            index: -1,
            column: 0,
            line: 0
        };
        easing = "cubic-bezier(0.165, 0.840, 0.440, 1.000)";
        time = 400;
        grid.on("touchend", "a", function(e) {
            var column, el, image, index, info, item, line;
            el = $(e.currentTarget);
            info = el.find(".info");
            image = el.find("img");
            index = el.parent().index();
            column = index % columns;
            line = Math.floor(index / columns);
            //console.log(index, column, line);
            item = {
                el: el,
                index: index,
                column: column,
                line: line,
                info: info,
                image: image
            };
            if (current.el && current.index === index) return;
            if (line === current.line && column > current.column) {
                showItem(item, "-100%", 0, "25%", 0);
                hideItem(current, "100%", 0, "-25%", 0);
            } else if (line === current.line && column < current.column) {
                showItem(item, "100%", 0, "-25%", 0);
                hideItem(current, "-100%", 0, "25%", 0);
            } else if (line > current.line && column === current.column) {
                showItem(item, 0, "-100%", 0, "25%");
                hideItem(current, 0, "100%", 0, "-25%");
            } else {
                showItem(item, 0, "100%", 0, "-25%");
                hideItem(current, 0, "-100%", 0, "25%");
            }
            return current = item;
        });
        grid.on("mouseenter", "a", function(e) {
            var column, el, image, index, info, item, line;
            el = $(e.currentTarget);
            info = el.find(".info");
            image = el.find("img");
            index = el.parent().index();
            column = index % columns;
            line = Math.floor(index / columns);
            //console.log(index, column, line);
            item = {
                el: el,
                index: index,
                column: column,
                line: line,
                info: info,
                image: image
            };
            if (current.el && current.index === index) return;
            if (line === current.line && column > current.column) {
                showItem(item, "-100%", 0, "25%", 0);
                hideItem(current, "100%", 0, "-25%", 0);
            } else if (line === current.line && column < current.column) {
                showItem(item, "100%", 0, "-25%", 0);
                hideItem(current, "-100%", 0, "25%", 0);
            } else if (line > current.line && column === current.column) {
                showItem(item, 0, "-100%", 0, "25%");
                hideItem(current, 0, "100%", 0, "-25%");
            } else {
                showItem(item, 0, "100%", 0, "-25%");
                hideItem(current, 0, "-100%", 0, "25%");
            }
            return current = item;
        });
        showItem = function(item, infoX, infoY, imageX, imageY) {
            item.info.stop().css({
                x: infoX,
                y: infoY,
                display: "block"
            }).transition({
                    x: 0,
                    y: 0,
                    duration: time,
                    easing: easing
                });
            return item.image.stop().transition({
                x: imageX,
                y: imageY,
                opacity: .5,
                duration: time,
                easing: easing
            });
        };
        return hideItem = function(item, infoX, infoY, imageX, imageY) {
            if (!item.el) return;
            item.info.stop().transition({
                x: infoX,
                y: infoY,
                duration: time,
                easing: easing
            });
            return item.image.stop().css({
                x: imageX,
                y: imageY,
                opacity: .5
            }).transition({
                    x: 0,
                    y: 0,
                    opacity: 1,
                    duration: time,
                    easing: easing
                });
        };
    });
}).call(this);