﻿using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http.Internal;
using SFA.DAS.RoatpFinance.Web.ApplyTypes.Apply;
using SFA.DAS.RoatpFinance.Web.ViewModels;
using SFA.DAS.RoatpFinance.Web.Validators;

namespace SFA.DAS.RoatpFinance.Web.Tests.Validators
{
    public class RoatpFinancialClarificationViewModelValidatorTests
    {
        private RoatpFinancialClarificationViewModelValidator _validator = new RoatpFinancialClarificationViewModelValidator();
        private const int MaxWordCount = 500;

        [Test]
        public void Validator_rejects_missing_SelectedGrade()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = null
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "FinancialReviewDetails.SelectedGrade");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_missing_InadequateComments_when_graded_Inadequate()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                InadequateComments = null,
                InadequateExternalComments = "external comments",
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Inadequate,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "InadequateComments");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_InadequateComments_above_maxwordcount_when_graded_Inadequate()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                InadequateComments = string.Join(" ", Enumerable.Repeat("a", MaxWordCount + 1)),
                InadequateExternalComments = "external comments",
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Inadequate,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "InadequateComments");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_missing_InadequateExternalComments_when_graded_Inadequate()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                InadequateComments = "comments",
                InadequateExternalComments = null,
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Inadequate,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "InadequateExternalComments");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_InadequateExternalComments_above_maxwordcount_when_graded_Inadequate()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                InadequateComments = "comments",
                InadequateExternalComments = string.Join(" ", Enumerable.Repeat("a", MaxWordCount + 1)),
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Inadequate,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "InadequateExternalComments");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_missing_FinancialDueDate_when_graded_Outstanding()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                OutstandingFinancialDueDate = new FinancialDueDate { },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Outstanding,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "OutstandingFinancialDueDate");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_unparseable_FinancialDueDate_when_graded_Outstanding()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                OutstandingFinancialDueDate = new FinancialDueDate
                {
                    Day = "test",
                    Month = "test2",
                    Year = "test3"
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Outstanding,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "OutstandingFinancialDueDate");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_invalid_FinancialDueDate_when_graded_Outstanding()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                OutstandingFinancialDueDate = new FinancialDueDate
                {
                    Day = "999",
                    Month = DateTime.Today.Month.ToString(),
                    Year = DateTime.Today.Year.ToString()
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Outstanding,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "OutstandingFinancialDueDate");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_FinancialDueDate_before_today_when_graded_Outstanding()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                OutstandingFinancialDueDate = new FinancialDueDate
                {
                    Day = DateTime.Today.AddDays(-1).Day.ToString(),
                    Month = DateTime.Today.AddDays(-1).Month.ToString(),
                    Year = DateTime.Today.AddDays(-1).Year.ToString()
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Outstanding,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "OutstandingFinancialDueDate");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_missing_FinancialDueDate_when_graded_Good()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                GoodFinancialDueDate = new FinancialDueDate { },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Good,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "GoodFinancialDueDate");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_unparseable_FinancialDueDate_when_graded_Good()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                GoodFinancialDueDate = new FinancialDueDate
                {
                    Day = "test",
                    Month = "test",
                    Year = "test"
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Good,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "GoodFinancialDueDate");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_invalid_FinancialDueDate_when_graded_Good()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                GoodFinancialDueDate = new FinancialDueDate
                {
                    Day = "999",
                    Month = DateTime.Today.Month.ToString(),
                    Year = DateTime.Today.Year.ToString()
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Good,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "GoodFinancialDueDate");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_FinancialDueDate_before_today_when_graded_Good()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                GoodFinancialDueDate = new FinancialDueDate
                {
                    Day = DateTime.Today.AddDays(-1).Day.ToString(),
                    Month = DateTime.Today.AddDays(-1).Month.ToString(),
                    Year = DateTime.Today.AddDays(-1).Year.ToString()
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Good,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "GoodFinancialDueDate");
            error.Should().NotBeNull();
        }


        [Test]
        public void Validator_rejects_missing_FinancialDueDate_when_graded_Satisfactory()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                SatisfactoryFinancialDueDate = new FinancialDueDate { },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Satisfactory,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "SatisfactoryFinancialDueDate");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_unparseable_FinancialDueDate_when_graded_Satisfactory()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                SatisfactoryFinancialDueDate = new FinancialDueDate
                {
                    Day = "test",
                    Month = "test",
                    Year = "test"
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Satisfactory,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "SatisfactoryFinancialDueDate");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_invalid_FinancialDueDate_when_graded_Satisfactory()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                SatisfactoryFinancialDueDate = new FinancialDueDate
                {
                    Day = "999",
                    Month = DateTime.Today.Month.ToString(),
                    Year = DateTime.Today.Year.ToString()
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Satisfactory,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "SatisfactoryFinancialDueDate");
            error.Should().NotBeNull();
        }

        [Test]
        public void Validator_rejects_FinancialDueDate_before_today_when_graded_Satisfactory()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                SatisfactoryFinancialDueDate = new FinancialDueDate
                {
                    Day = DateTime.Today.AddDays(-1).Day.ToString(),
                    Month = DateTime.Today.AddDays(-1).Month.ToString(),
                    Year = DateTime.Today.AddDays(-1).Year.ToString()
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Satisfactory,
                }
            };

            var validationResponse = _validator.Validate(_viewModel, false, true);

            var error = validationResponse.Errors.FirstOrDefault(x => x.Field == "SatisfactoryFinancialDueDate");
            error.Should().NotBeNull();
        }



        [Test]
        public void When_FilesToUpload_has_file_that_exceeds_maximum_filesize_then_an_error_is_returned()
        {
            const int currentMaxFileSizeInBytes = 5 * 1024 * 1024;

            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                SatisfactoryFinancialDueDate = new FinancialDueDate
                {
                    Day = DateTime.Today.AddDays(-1).Day.ToString(),
                    Month = DateTime.Today.Month.ToString(),
                    Year = DateTime.Today.Year.ToString()
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Satisfactory,
                }
            };
            _viewModel.FilesToUpload = new FormFileCollection
            {
                GenerateClarificationFile("ClarificationFile.pdf", true, currentMaxFileSizeInBytes + 1)
            };

            var response = _validator.Validate(_viewModel, true, false);

            Assert.IsFalse(response.IsValid);
            Assert.AreEqual("The selected file must be smaller than 5MB", response.Errors.First().ErrorMessage);
            Assert.AreEqual("ClarificationFile", response.Errors.First().Field);
        }



        [Test]
        public void When_FilesToUpload_has_file_that_is_not_a_pdf_then_an_error_is_returned()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                SatisfactoryFinancialDueDate = new FinancialDueDate
                {
                    Day = DateTime.Today.AddDays(-1).Day.ToString(),
                    Month = DateTime.Today.Month.ToString(),
                    Year = DateTime.Today.Year.ToString()
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Satisfactory,
                }
            };
            _viewModel.FilesToUpload = new FormFileCollection
            {
                GenerateClarificationFile("ClarificationFile.txt", false, 10)
            };

            var response = _validator.Validate(_viewModel, true, false);

            Assert.IsFalse(response.IsValid);
            Assert.AreEqual("The selected file must be a PDF", response.Errors.First().ErrorMessage);
            Assert.AreEqual("ClarificationFile", response.Errors.First().Field);
        }


        [Test]
        public void When_FilesToUpload_has_no_file()
        {
            var _viewModel = new RoatpFinancialClarificationViewModel
            {
                SatisfactoryFinancialDueDate = new FinancialDueDate
                {
                    Day = DateTime.Today.AddDays(-1).Day.ToString(),
                    Month = DateTime.Today.Month.ToString(),
                    Year = DateTime.Today.Year.ToString()
                },
                FinancialReviewDetails = new FinancialReviewDetails
                {
                    SelectedGrade = FinancialApplicationSelectedGrade.Satisfactory,
                }
            };
            _viewModel.FilesToUpload = new FormFileCollection();

            var response = _validator.Validate(_viewModel, true, false);

            Assert.IsFalse(response.IsValid);
            Assert.AreEqual("Select a file", response.Errors.First().ErrorMessage);
            Assert.AreEqual("ClarificationFile", response.Errors.First().Field);
        }

        private static FormFile GenerateClarificationFile(string fileName, bool hasPdfHeader, int length)
        {
            var pdfHeader = new byte[] { 0x25, 0x50, 0x44, 0x46 };

            MemoryStream fileContent = new MemoryStream();

            if (hasPdfHeader)
            {
                fileContent.Write(pdfHeader);
            }

            var remainingContentToGenerate = length - (int)fileContent.Length;

            if (remainingContentToGenerate > 0)
            {
                var contentToGenerate = Enumerable.Repeat((byte)0x20, remainingContentToGenerate);
                fileContent.Write(contentToGenerate.ToArray());
            }

            return new FormFile(fileContent, 0, fileContent.Length, fileName, fileName);
        }
    }
}
