// Для образования
import {KafedraFields} from '../enums/kafedra.enum';

export class ArrayHelper {
  static DisplayShortNameForDuplicateKafedras(kafedras: any) {
    return kafedras.sort(function (a: any, b: any) {
      if (a.kafedraName.includes(b.kafedraName) || b.kafedraName.includes(a.kafedraName)) {
        const [aValue, bValue] = ArrayHelper.handleKafedraNames(
          kafedras,
          a,
          b,
          KafedraFields.kafedraName,
          KafedraFields.facultySName
        );
        return aValue || bValue;
      }
      return a.kafedraName.localeCompare(b.kafedraName);
    });
  }

  // For SName field (для текущего, промежуточного контроля)
  static DisplaySNameForDuplicateKafedras(
    kafedras: any,
    nameField: KafedraFields,
    sNameField: KafedraFields
  ) {
    return kafedras.sort(function (a: any, b: any) {
      if (a[nameField] === b[nameField] || b[nameField] === a[nameField]) {
        const [aValue, bValue] = ArrayHelper.handleKafedraNames(kafedras, a, b, nameField, sNameField);
        return aValue || bValue;
      }
      return a[nameField].localeCompare(b[nameField]);
    });
  }

  static handleKafedraNames(kafedras: any, a: any, b: any, nameField: string, sNameField: string) {
    const indexA = kafedras.findIndex(
      (item: any) =>
        item[nameField] === a[nameField] &&
        item.facultyId === a.facultyId &&
        item[sNameField] === a[sNameField]
    );
    const indexB = kafedras.findIndex(
      (item: any) =>
        item[nameField] === b[nameField] &&
        item.facultyId === b.facultyId &&
        item[sNameField] === b[sNameField]
    );

    if (!kafedras[indexA][sNameField]?.includes('Кафедра')) {
      kafedras[indexA][nameField] = a[nameField] + ` (${a[sNameField]})`;
    }

    if (!kafedras[indexB][sNameField]?.includes('Кафедра')) {
      kafedras[indexB][nameField] = b[nameField] + ` (${b[sNameField]})`;
    }
    return [kafedras[indexA][nameField], kafedras[indexB][nameField]];
  }

  static difference<T extends object>(arr1: T[], arr2: T[]) {
    return arr1.filter((x) => !arr2.includes(x)) as T;
  }

  static keyBy<
    A extends object,
    K extends keyof {[P in keyof A as A[P] extends PropertyKey ? P : never]: unknown}
  >(array: A[], key: string) {
    return array
      ? array.reduce(
          (r, x) => ({...r, [x[key as K] as unknown as PropertyKey]: x}),
          {} as {[P in A[K] as A[K] extends PropertyKey ? A[K] : never]: A}
        )
      : [];
  }

  private static sortByMultipleKeys<T extends object, K extends keyof T>(
    keys: string[],
    orders: string[] = []
  ) {
    return (a: T, b: T): number => {
      if (!keys.length) return 0;
      const key = (<K[]>keys)[0];
      const order = orders[0];
      const isBIndexLess = order === 'desc' ? a[key] < b[key] : a[key] > b[key];
      const isAIndexLess = order === 'desc' ? b[key] < a[key] : b[key] > a[key];
      return isBIndexLess
        ? 1
        : isAIndexLess
        ? -1
        : ArrayHelper.sortByMultipleKeys(keys.slice(1), orders.slice(1))(a, b);
    };
  }

  static sortBy<T extends object>(array: T[], keys: string[], orders?: string[]) {
    return array.concat().sort((a, b) => ArrayHelper.sortByMultipleKeys(keys, orders)(a, b));
  }
}
