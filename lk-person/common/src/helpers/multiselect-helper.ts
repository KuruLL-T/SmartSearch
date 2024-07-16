export class MultiselectHelper {
  static valueChangeBySourceName(
    array: unknown[],
    sourceProperty: string,
    sourceValue: unknown,
    targetValue: unknown
  ): void {
    if (!array || array.length == 0 || !sourceProperty) return;

    for (let i = 0; i < array.length; i++) {
      if (Array.isArray(array) && array[i][sourceProperty] == sourceValue)
        array[i][sourceProperty] = targetValue;
    }
  }

  static valueChange(array: unknown[], sourceValue: unknown, targetValue: unknown): void {
    if (!array || array.length == 0) return;

    for (let i = 0; i < array.length; i++) {
      if (array[i] === sourceValue) array[i] = targetValue;
    }
  }

  static arrayRewrite(array: unknown[], sourceProperty: string | null = null): unknown[] {
    if (!array || array.length == 0) return [];

    if (
      (Array.isArray(array) && sourceProperty != null && array[array.length - 1][sourceProperty] === '') ||
      array[array.length - 1] === ''
    )
      return sourceProperty == null
        ? array.filter((_: unknown) => _ === '')
        : array.filter((_: object) => _[sourceProperty as keyof unknown] === '');
    if (
      (sourceProperty != null && array[array.length - 1][sourceProperty] === 0) ||
      array[array.length - 1] === 0
    )
      return sourceProperty == null
        ? array.filter((_: any) => _ === 0)
        : array.filter((_: any) => _[sourceProperty] === 0);

    return sourceProperty == null
      ? array.filter((_: unknown) => _ !== '' && _ !== 0)
      : array.filter((_: object) => _[sourceProperty as keyof unknown] !== '' && _[sourceProperty as keyof unknown] !== 0);
  }

  /**
   * Определить тип данных перед отправкой в перезапись массив
   *
   * @param source - Источник данных
   * @param value - Значение
   * @param sourceProperty - Название поля для фильтрации
   */
  static detectTypeForRewrite(source: unknown, value: never[], sourceProperty?: string) {
    return Array.isArray(source) ? MultiselectHelper.arrayRewrite(value, sourceProperty) : value;
  }

  /**
   * Отобразить значения в режиме редактирования
   *
   * @param source - Источник данных
   * @param targetKey - Название поля для фильтрации
   * @returns Массив с заданным ключом
   * @example
   * Пример динамической передачи данных в заданные переменные:
   * ```ts
   * public courses: any[] = [];
   * public faculties: any[] = [];
   * public departments: any[] = [];
   *
   * public coursesEdit: any;
   * public facultiesEdit: any;
   * public departmentsEdit: any;
   * ...
   *
   * editHandler(dataItem: any) {
   *
   *    const dataSources: any[] = ['courses', 'faculties', 'departments'];
   *    const keys: any[] = ['courseNumber','facultyId', 'departmentId'];
   *
   *    for(let i = 0; i < dataSources.length; i++) {
   *        (this as any)[`${dataSources[i]}Edit`] = DisplayValuesInEditMode((dataItem[dataSources[i]]), [keys[i]]);
   *    }
   *
   *    ...
   * }
   *
   * ```
   */

  static DisplayValuesInEditMode(source: unknown[], targetKey: string) {
    return !source ||
      source.map((x: unknown) => x instanceof Object && x[targetKey as keyof unknown]).length === 0
      ? ['']
      : source.map((x: unknown) => x instanceof Object && x[targetKey as keyof unknown]);
  }

  // static DisplayValuesInEditMode(source: any, targetSource: any, sourceKey: string, targetKey: string) {
  //   let arr: any[] = [];
  //   for (let i = 0; i < targetSource.length; i++) {
  //     let id = source.find((item: { [x: string]: any; }) => item[`${sourceKey}`] === targetSource[i][targetKey])?.[sourceKey];
  //     if (id) arr.push(id);
  //   }
  //   return arr;
  // }
}
