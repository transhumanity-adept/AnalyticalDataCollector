export interface ITaskModel {
  id: String,
  createDate: Date,
  fromArticleId: Number,
  toArticleId: Number,
  currentArticleIdLoad: Number,
  readonly isCompleted: Boolean
}

export class TaskModel implements ITaskModel {

  constructor(id: String, createDate: Date, currentArticleIdLoad: Number, fromArticleId: Number, toArticleId: Number) {
    this._id = id;
    this._createDate = createDate;
    this._currentArticleIdLoad = currentArticleIdLoad;
    this._fromArticleId = fromArticleId;
    this._toArticleId = toArticleId;
  }

  private _createDate: Date;
  private _currentArticleIdLoad: Number;
  private _fromArticleId: Number;
  private _id: String;
  private _toArticleId: Number;

  get toArticleId(): Number {
    return this._toArticleId;
  }
  set toArticleId(value: Number) {
    this._toArticleId = value;
  }

  get id(): String {
    return this._id;
  }
  set id(value: String) {
    this._id = value;
  }

  get fromArticleId(): Number {
    return this._fromArticleId;
  }
  set fromArticleId(value: Number) {
    this._fromArticleId = value;
  }

  get currentArticleIdLoad(): Number {
    return this._currentArticleIdLoad;
  }
  set currentArticleIdLoad(value: Number) {
    this._currentArticleIdLoad = value;
  }

  get createDate(): Date {
    return this._createDate;
  }
  set createDate(value: Date) {
    this._createDate = value;
  }

  get isCompleted(): Boolean {
    return this._currentArticleIdLoad === this._toArticleId;
  }

}
