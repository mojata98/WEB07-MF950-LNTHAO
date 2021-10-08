/**
 * Tình trạng Form Employee
 */
export const EMPLOYEEFORM_STATE = {
    ADD: 0,
    EDIT: 1,
}

/**
 * Tình trạng Popup
 */
export const POPUP_STATE = {
    VALIDATE: 1,
    DUPLICATE_EMPLOYEECODE: 2,
    DELETE: 3,
    UPDATE_DATA: 4,
}

/**
 * Trạng thái mã code
 */
export const STATUS_CODE = {
    SUCCESS: 200,
    NO_CONTENT: 204,
    BAD_REQUEST: 400,
    ERROR_SERVER: 500,
    NOT_FOUND: 404,
    CREATED: 201,
}