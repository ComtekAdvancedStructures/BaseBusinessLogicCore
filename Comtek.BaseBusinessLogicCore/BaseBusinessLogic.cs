using System;
using AES.HelpersCore;
using Comtek.DataOperationResultCore;
using Microsoft.EntityFrameworkCore;

namespace Comtek.BaseBusinessLogicCore
{
    public abstract class BaseBusinessLogic<T> where T : DbContext
    {
        private readonly string successMessage;
        private readonly string failMessage;
        public T Context { get; }

        protected BaseBusinessLogic(T context, string appName)
        {
            Context = context;
            successMessage = $"Changes saved to {appName} database";
            failMessage = $"There was an error saving the changes to the {appName} database.";
        }

        public DataOperationResult Save()
        {
            var result = new DataOperationResult();
            try
            {
                Context.SaveChanges();
                result.Success = true;
                result.Messages.Add(successMessage);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Messages.Add(failMessage);
                result.Messages.Add(ErrorHelper.GetExceptionMessage(ex));
            }
            return result;
        }

        public DataOperationResult SaveAsync()
        {
            var result = new DataOperationResult();
            try
            {
                Context.SaveChangesAsync();
                result.Success = true;
                result.Messages.Add(successMessage);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Messages.Add(failMessage);
                result.Messages.Add(ErrorHelper.GetExceptionMessage(ex));
            }
            return result;
        }
    }
}