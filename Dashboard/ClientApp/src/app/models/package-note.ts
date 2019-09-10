export class PackageNote {
  packageNoteId: number;
  packaging: string;
  stagingArea: string;
  packer: string;

  static fromJson(packageNote: any): PackageNote {
    return {
      packageNoteId: packageNote.PackageNoteId || packageNote.packageNoteId,
      packaging: packageNote.Packaging || packageNote.packaging,
      stagingArea: packageNote.StagingArea || packageNote.stagingArea,
      packer: packageNote.Packer || packageNote.packer
    }
  }
}