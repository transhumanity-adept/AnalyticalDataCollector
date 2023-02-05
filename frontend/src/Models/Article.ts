export type Article = {
  id: Number,
  end: String,
  title: String,
  authors: Array<String>,
  orgNames: Array<String>,
  type: String,
  language: String,
  year: String,
  keyWords: Array<String>,
  annonation: String,
  bibliometricIndicators: BibliometricIndicators,
  altmetrics: Altmetrics
}

export type BibliometricIndicators = {
  rubricGRNTI: String,
  thematicArea: String,
  inRSCI: Boolean,
  numberOfCitationsInRSCI: Number,
  inCoreRSCI: Boolean,
  numberOfCitationsInCoreRSCI: Number
}

export type Altmetrics = {
  views: Number,
  numberUploads: Number
}
