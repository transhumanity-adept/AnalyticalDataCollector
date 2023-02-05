export interface IArticleLoadLog {
  articleId: Number,
  errorMessage: String,
  logType: LogType
}

export class ArticleLoadLog implements IArticleLoadLog {
  get errorMessage(): String {
    return this._errorMessage;
  }

  get articleId(): Number {
    return this._articleId;
  }

  get logType(): LogType {
    return this._logType;
  }

  constructor(articleId: Number, errorMessage: String, logType: LogType) {
    this._articleId = articleId;
    this._errorMessage = errorMessage;
    this._logType = logType;
  }
  private readonly _logType: LogType;
  private readonly _articleId: Number
  private readonly _errorMessage: String
}

export enum LogType {
  Error = "Error",
  Warning = "Warning",
  Info = "Info"
}
