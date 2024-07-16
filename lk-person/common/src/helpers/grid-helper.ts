export class GridHelper {
  // Отображение списка в таблицу
  static GridDisplayList(dataItem: unknown[], key: string) {
    let string = '';
    if (Array.isArray(dataItem)) {
      for (let i = 0; i < dataItem.length; i++) {
        string += dataItem[i][key] + ', ';
      }
    }
    return dataItem.length ? string.slice(0, -2) : 'Все';
  }
}
