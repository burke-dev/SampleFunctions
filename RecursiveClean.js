"use strict";

function cleanParentObject(jsonObject) {
  try {
    return _cleanChild(jsonObject);
  }
  catch (err) {
    throw new Error(`cleanParentObject failed\r\n${jsonObject}\r\n${err}`);
  }
}

function _cleanChild(obj) {
  if (!obj) {
    return obj;
  }
  if (typeof obj === 'string') {
    return obj.trim();
  }
  if (typeof obj === 'number') {
    return obj;
  }
  if (Array.isArray(obj)) {
    return obj.map(sub => _cleanChild(sub)).filter(x => x);
  }
  if (typeof obj === 'object') {
    let newObj = {};
    Object.keys(obj).map(sub => {
      if(obj[sub]) {
        newObj[sub] = _cleanChild(obj[sub]);
      }
    });
    return newObj;
  }
  return obj;
}
