export class ElementRefHelper {
  static hasClass = (el: HTMLElement, className: string) => new RegExp(className).test(el.className);

  static isChildOf = (el: HTMLElement, className: string) => {
    while (el && el.parentElement) {
      if (ElementRefHelper.hasClass(el.parentElement, className)) {
        return true;
      }
      el = el.parentElement;
    }
    return false;
  };

  static getParentElement = (el: HTMLElement, className: string) => {
    while (el && el.parentElement) {
      if (ElementRefHelper.hasClass(el.parentElement, className)) {
        return el.parentElement as HTMLElement;
      }
      el = el.parentElement;
    }
    return null;
  };
}
