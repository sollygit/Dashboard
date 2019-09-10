export class SortSettings<T> {
  column?: keyof T = undefined;
  ascending: boolean = false;
}

export interface Comparer<T> {
  compare: (a: T, b: T) => number;
}

export interface MultipleSortProperty<T> {
  asc: boolean;
  comparer: Comparer<T>;
}

export function sorterFactory<T>(comparer: Comparer<T>, ascending?: boolean): (a: T, b: T) => number {
  return (a: T, b: T): number => {
    return (ascending ? 1 : -1) * comparer.compare(a, b);
  };
}

export function orderByMultiple<T>(...props: Array<MultipleSortProperty<T>>): (a: T, b: T) => number {
  const prop = props.shift();
  if (!prop)
    throw new Error('At least one property needs to be provided');
  return function (a: T, b: T): number {
    let equality = (prop.asc ? 1 : -1) * prop.comparer.compare(a, b);
    if (equality === 0 && props.length > 0)
      return orderByMultiple<T>(...props)(a, b);
    return equality;
  };
}