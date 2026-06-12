namespace FormulariosUAF.Models.Enums;

public enum TaxFolderAlertType
{
    RutMismatch = 0,
    BusinessNameMismatch = 1,
    LegalRepMismatch = 2,
    PersonNotInFolder = 3,
    UnknownPersonInFolder = 4,
    DocumentUnreadable = 5,
    WrongDocument = 6,
    OldDocument = 7
}
