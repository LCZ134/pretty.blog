(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports) :
  typeof define === 'function' && define.amd ? define(['exports'], factory) :
  (global = global || self, factory(global.R = {}));
}(this, function (exports) { 'use strict';

  function start() {
    this._isStop = false;
    var self = this;
    if (self._timer) return;
    self._timer = setInterval(function () {
      self._render.clear();

      Object.keys(self._danMus).forEach(function (dmType) {
        return self._danMus[dmType].forEach(function (dms) {
          return dms.forEach(function (i) {
            if (!i) return;
            self._animation.move(i);
            self._render.render(i);
          });
        });
      });
    }, 17.7);
  }

  function stop() {
    this._isStop = true;
    clearInterval(this._timer);
    this._timer = null;
  }

  var GlobalApi = {
    start: start,
    stop: stop,
    push: function push(dm) {
      if (Array.isArray(dm)) {
        this._dispatcher.pushArr(dm);
      } else {
        this._dispatcher.push(dm);
      }
    },
    dispose: function dispose(dm) {
      this._dispatcher.dispose(dm);
    }
  };

  var _typeof = typeof Symbol === "function" && typeof Symbol.iterator === "symbol" ? function (obj) {
    return typeof obj;
  } : function (obj) {
    return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj;
  };

  var classCallCheck = function (instance, Constructor) {
    if (!(instance instanceof Constructor)) {
      throw new TypeError("Cannot call a class as a function");
    }
  };

  var createClass = function () {
    function defineProperties(target, props) {
      for (var i = 0; i < props.length; i++) {
        var descriptor = props[i];
        descriptor.enumerable = descriptor.enumerable || false;
        descriptor.configurable = true;
        if ("value" in descriptor) descriptor.writable = true;
        Object.defineProperty(target, descriptor.key, descriptor);
      }
    }

    return function (Constructor, protoProps, staticProps) {
      if (protoProps) defineProperties(Constructor.prototype, protoProps);
      if (staticProps) defineProperties(Constructor, staticProps);
      return Constructor;
    };
  }();

  var defineProperty = function (obj, key, value) {
    if (key in obj) {
      Object.defineProperty(obj, key, {
        value: value,
        enumerable: true,
        configurable: true,
        writable: true
      });
    } else {
      obj[key] = value;
    }

    return obj;
  };

  var Canvas = function () {
    function Canvas() {
      classCallCheck(this, Canvas);

      this.ctx = null;
      this.el = null;
    }

    createClass(Canvas, [{
      key: 'mount',
      value: function mount(selector, options) {
        options = options || {};
        this.el = document.createElement('canvas');
        this.width = selector.offsetWidth;
        this.height = selector.offsetHeight;
        this.lineHeight = options.lineHeight || 30;
        this.el.width = this.width;
        this.el.height = this.height;
        selector.appendChild(this.el);
        this.ctx = this.el.getContext('2d');
      }
    }, {
      key: 'clear',
      value: function clear() {
        var height = this.height;

        this.el.height = height;
      }
    }]);
    return Canvas;
  }();

  var canvas = new Canvas();

  var ScrollDispatcher = function () {
    function ScrollDispatcher(context) {
      classCallCheck(this, ScrollDispatcher);

      this.type = 'scroll';
      this.context = context;
    }

    createClass(ScrollDispatcher, [{
      key: 'update',
      value: function update(rows, row, dm) {
        var baseUpdate = this.context.update.bind(this.context);
        dm.row = row;
        rows.push(dm);
        baseUpdate(dm);
      }
    }, {
      key: 'dispatch',
      value: function dispatch(dms, dm) {
        for (var row = 0; row < this.context._rowCount; row++) {
          if (!dms) {
            dms = [];
          }
          if (!dms[row]) {
            dms[row] = [];
          }
          var rows = dms[row];

          if (rows.length <= 0) {
            this.update(rows, row, dm);
            break;
          }
          if (!rows[rows.length - 1]) {
            this.update(rows, row, dm);
            break;
          }
          var point = rows[rows.length - 1].point;
          if (point && point.x + dm.content.length * dm.fontSize + this.context.ctx._options.distance < canvas.width) {
            this.update(rows, row, dm);
            break;
          }
        }
      }
    }, {
      key: 'dispose',
      value: function dispose(dms, dm) {
        var rows = dms[dm.row];
        rows[rows.indexOf(dm)] = null;
      }
    }]);
    return ScrollDispatcher;
  }();

  var ScrollDispatcher$1 = function () {
    function ScrollDispatcher(context) {
      classCallCheck(this, ScrollDispatcher);

      this.type = 'top';
      this.context = context;
    }

    createClass(ScrollDispatcher, [{
      key: 'dispatch',
      value: function dispatch(dms, dm) {
        var update = this.context.update;

        update = update.bind(this.context);
        if (!dms) {
          dms = [];
        }
        for (var row = 0; row < this.context._rowCount; row++) {

          if (!dms[row]) {
            dms[row] = [];
          }
          var item = dms[row][0];
          if (!item) {
            dms[row][0] = dm;
            dm.row = row;
            update(dm);
            break;
          }
        }
      }
    }, {
      key: 'dispose',
      value: function dispose(dms, dm) {
        for (var i = 0; i < dms.length; i++) {
          if (dms[i][0] === dm) {
            dms[i][0] = null;
            break;
          }
        }
      }
    }]);
    return ScrollDispatcher;
  }();

  var Dispatcher = function () {
    function Dispatcher(ctx) {
      var _dispatchers;

      classCallCheck(this, Dispatcher);

      this._danMus = [];
      this._subs = [];
      this._loadedDanMus = [];
      this.ctx = ctx;
      this._rowCount = this.ctx._options.rowCount || canvas.height / canvas.lineHeight - 1;

      var scroll = new ScrollDispatcher(this);
      var top = new ScrollDispatcher$1(this);
      this.dispatchers = (_dispatchers = {}, defineProperty(_dispatchers, scroll.type, scroll), defineProperty(_dispatchers, top.type, top), _dispatchers);
    }

    createClass(Dispatcher, [{
      key: 'subscribe',
      value: function subscribe(cb) {
        this._subs.push(cb);
      }
    }, {
      key: 'notifyAll',
      value: function notifyAll() {
        var _this = this;

        this._subs.forEach(function (callback) {
          return callback(_this._loadedDanMus);
        });
      }
    }, {
      key: 'push',
      value: function push(dm) {
        if ((typeof dm === 'undefined' ? 'undefined' : _typeof(dm)) !== 'object') return;
        if (!this._danMus[dm.type]) {
          this._danMus[dm.type] = [];
        }
        if (!dm.type || !dm.content || !dm.fontSize || !dm.color) {
          return;
        }
        this._danMus[dm.type].push(dm);
        this.dispatch(dm.type);
      }
    }, {
      key: 'pushArr',
      value: function pushArr(dms) {
        if (!Array.isArray(dms)) return;

        dms.forEach(push);
      }
    }, {
      key: '_getloadedDMCount',
      value: function _getloadedDMCount() {
        var count = 0;
        this._loadedDanMus.forEach(function (rows) {
          return count += rows.filter(function (i) {
            return i;
          }).length;
        });
        return count;
      }
    }, {
      key: 'dispatch',
      value: function dispatch(type) {
        var self = this;
        if (self._danMus[type].length <= 0) return;
        var dm = self._danMus[type][0];
        var loadCount = this._getloadedDMCount();

        if (loadCount > this.ctx._options.maxCount) {
          return;
        }
        var dispatcher = this.dispatchers[dm.type];
        if (!dispatcher) {
          this.removeItem(this._danMus, dm);
          return;
        }
        if (!this._loadedDanMus[dm.type]) {
          this._loadedDanMus[dm.type] = [];
        }
        dispatcher.dispatch(this._loadedDanMus[dm.type], dm);
      }
    }, {
      key: 'update',
      value: function update(dm) {
        this.removeItem(this._danMus[dm.type], dm);
        this.notifyAll();
      }
    }, {
      key: 'removeItem',
      value: function removeItem(arr, dm) {
        arr.splice(arr.indexOf(dm), 1);
      }
    }, {
      key: 'dispose',
      value: function dispose(dm) {
        this.dispatchers[dm.type].dispose(this._loadedDanMus[dm.type], dm);

        if (this._danMus.length > 0) {
          this.dispatch(dm.type);
        }
      }
    }]);
    return Dispatcher;
  }();

  var ScrollAnimation = function () {
    function ScrollAnimation(context) {
      classCallCheck(this, ScrollAnimation);

      this.type = 'scroll';
      this.context = context;
    }

    createClass(ScrollAnimation, [{
      key: 'init',
      value: function init(dm) {
        var width = canvas.width,
            lineHeight = canvas.lineHeight;

        var s = 3 + Math.floor(Math.random() * 3);
        var point = {
          x: width,
          y: lineHeight * (dm.row + 1),
          speed: s
        };

        dm.point = point;
      }
    }, {
      key: 'move',
      value: function move(dm) {
        var point = dm.point;

        point.x = point.x - point.speed;
        var dmLen = dm.content.length * dm.fontSize;
        if (point.x < -dmLen) {
          this.context.ctx.dispose(dm);
        }
        if (point.x + dmLen + this.context.ctx._options.distance < canvas.width) {
          this.context.ctx._dispatcher.dispatch(dm.type);
        }
      }
    }]);
    return ScrollAnimation;
  }();

  var TopAnimation = function () {
    function TopAnimation(context) {
      classCallCheck(this, TopAnimation);

      this.type = 'top';
      this.context = context;
    }

    createClass(TopAnimation, [{
      key: 'init',
      value: function init(dm) {
        var _this = this;

        var width = canvas.width,
            lineHeight = canvas.lineHeight;

        var s = 3 + Math.floor(Math.random() * 3);
        var point = {
          x: (width - dm.content.length * dm.fontSize) / 2,
          y: lineHeight * (dm.row + 1),
          speed: s
        };

        dm.point = point;

        setTimeout(function () {
          _this.context.ctx._dispatcher.dispose(dm);
          _this.context.ctx._dispatcher.dispatch(dm.type);
        }, 3000);
      }
    }, {
      key: 'move',
      value: function move(dm) {}
    }]);
    return TopAnimation;
  }();

  var Animation = function () {
    function Animation(ctx) {
      var _animations;

      classCallCheck(this, Animation);

      this.ctx = ctx;
      this.move = this.move.bind(this);

      var scrollAnimation = new ScrollAnimation(this);
      var topAnimation = new TopAnimation(this);

      this.animations = (_animations = {}, defineProperty(_animations, scrollAnimation.type, scrollAnimation), defineProperty(_animations, topAnimation.type, topAnimation), _animations);
    }

    createClass(Animation, [{
      key: 'move',
      value: function move(dm) {
        var animation = this.animations[dm.type];
        if (!animation) {
          this.ctx._dispatcher.dispose(dm);
          return;
        }
        if (!dm.point) {
          this.animations[dm.type].init(dm);
        } else {
          this.animations[dm.type].move(dm);
        }
      }
    }]);
    return Animation;
  }();

  var Render = function () {
    function Render() {
      classCallCheck(this, Render);
    }

    createClass(Render, [{
      key: 'render',
      value: function render(dm) {
        var ctx = canvas.ctx;

        var point = dm.point;
        if (!point) return;
        ctx.shadowColor = '#ccc';
        // 将阴影向右移动15px，向上移动10px
        ctx.shadowOffsetX = 2;
        ctx.shadowOffsetY = 2;
        // 轻微模糊阴影
        ctx.shadowBlur = 3;
        ctx.font = 'bold ' + dm.fontSize + 'px Consolas ';
        ctx.strokeStyle = '#gray';
        ctx.fillStyle = '' + dm.color;
        ctx.fillText(dm.content, point.x, point.y);
        ctx.strokeText(dm.content, point.x, point.y);
      }
    }, {
      key: 'clear',
      value: function clear() {
        canvas.clear();
      }
    }]);
    return Render;
  }();

  var PrettyDanMu = function PrettyDanMu(options) {
    var _this = this;

    classCallCheck(this, PrettyDanMu);

    options = options || {};
    this._options = options;
    this._danMus = [];

    Object.keys(GlobalApi).forEach(function (key) {
      _this[key] = GlobalApi[key];
    });

    if (!options.distance) {
      options.distance = 200;
    }

    if (!options.maxCount) {
      options.maxCount = 100;
    }

    if (options.el) {
      canvas.mount(options.el);
    }
    this._dispatcher = new Dispatcher(this);

    var self = this;
    this._dispatcher.subscribe(function (dms) {
      self._danMus = dms;
      if (!self._isStop && !self._timer) {
        self.start();
      }
    });

    this._animation = new Animation(this);
    this._render = new Render();
  };

  exports.default = PrettyDanMu;

  Object.defineProperty(exports, '__esModule', { value: true });

}));
//# sourceMappingURL=index.js.map
