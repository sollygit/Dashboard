export class Source {
  code: string;
  description: string;

  static fromJson(source: any): Source {
    return {
      code: source.Code,
      description: source.Description
    };
  }
}
