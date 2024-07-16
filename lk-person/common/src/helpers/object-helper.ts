export class ObjectHelper {
  static merge(current: any, updates: any) {
    for (const key of Object.keys(updates)) {
      if (!Object.prototype.hasOwnProperty.call(current, key) || typeof updates[key] !== 'object')
        current[key] = updates[key];
      else ObjectHelper.merge(current[key], updates[key]);
    }

    return current;
  }
}
