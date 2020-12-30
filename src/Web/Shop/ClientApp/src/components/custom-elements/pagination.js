import React, { Component, Fragment } from 'react';
import PropTypes from 'prop-types';

// ...

const LEFT_PAGE = 'LEFT';
const RIGHT_PAGE = 'RIGHT';

/**
 * Helper method for creating a range of numbers
 * range(1, 5) => [1, 2, 3, 4, 5]
 */
const range = (from, to, step = 1) => {
    let i = from;
    const range = [];

    while (i <= to) {
        range.push(i);
        i += step;
    }

    return range;
}

class Pagination extends Component {
    constructor(props) {
        super(props);
        const { currentPage = 1, totalPages = 20, pageSize = 20, pageNeighbours=1 } = props;

        this.currentPage = typeof currentPage === 'number' ? currentPage : 1;
        this.totalPages = typeof totalPages === 'number' ? totalPages : 1;
        this.pageSize = typeof pageSize === 'number' ? pageSize : 4;
        this.pageNeighbours = typeof pageNeighbours === 'number' ? pageNeighbours : 1;
    }

    /**
   * Let's say we have 10 pages and we set pageNeighbours to 2
   * Given that the current page is 6
   * The pagination control will look like the following:
   *
   * (1) < {4 5} [6] {7 8} > (10)
   *
   * (x) => terminal pages: first and last page(always visible)
   * [x] => represents current page
   * {...x} => represents page neighbours
   */
    fetchPageNumbers = () => {
        const totalPages = this.totalPages;
        const currentPage = this.currentPage;
        const pageNeighbours = this.pageNeighbours;

        /**
         * totalNumbers: the total page numbers to show on the control
         * totalBlocks: totalNumbers + 2 to cover for the left(<) and right(>) controls
         */
        const totalNumbers = (this.pageNeighbours * 2) + 3;
        const totalBlocks = totalNumbers + 2;

        if (totalPages > totalBlocks) {
            const startPage = Math.max(2, currentPage - pageNeighbours);
            const endPage = Math.min(totalPages - 1, currentPage + pageNeighbours);
            let pages = range(startPage, endPage);

            /**
             * hasLeftSpill: has hidden pages to the left
             * hasRightSpill: has hidden pages to the right
             * spillOffset: number of hidden pages either to the left or to the right
             */
            const hasLeftSpill = startPage > 2;
            const hasRightSpill = (totalPages - endPage) > 1;
            const spillOffset = totalNumbers - (pages.length + 1);

            switch (true) {
                // handle: (1) < {5 6} [7] {8 9} (10)
                case (hasLeftSpill && !hasRightSpill): {
                    const extraPages = range(startPage - spillOffset, startPage - 1);
                    pages = [LEFT_PAGE, ...extraPages, ...pages];
                    break;
                }

                // handle: (1) {2 3} [4] {5 6} > (10)
                case (!hasLeftSpill && hasRightSpill): {
                    const extraPages = range(endPage + 1, endPage + spillOffset);
                    pages = [...pages, ...extraPages, RIGHT_PAGE];
                    break;
                }

                // handle: (1) < {4 5} [6] {7 8} > (10)
                case (hasLeftSpill && hasRightSpill):
                default: {
                    pages = [LEFT_PAGE, ...pages, RIGHT_PAGE];
                    break;
                }
            }

            return [1, ...pages, totalPages];
        }

        return range(1, totalPages);
    }


    render() {
        //if (!this.totalRecords || this.totalPages === 1) return null;
        const pages = this.fetchPageNumbers();

        return (
            <Fragment>
                <nav aria-label="Countries Pagination">
                    <ul className="pagination">
                        {pages.map((page, index) => {

                            if (page === LEFT_PAGE) return (
                                <li key={index} className="page-item">
                                    <button className="page-link" aria-label="Previous" onClick={() =>
                                        this.props.handlePagination(this.currentPage - (this.pageNeighbours * 2) - 1)}>
                                        <span aria-hidden="true">&laquo;</span>
                                        <span className="sr-only">Previous</span>
                                    </button>
                                </li>
                            );

                            if (page === RIGHT_PAGE) return (
                                <li key={index} className="page-item">
                                    <button className="page-link" aria-label="Next" onClick={() =>
                                        this.props.handlePagination(this.currentPage + (this.pageNeighbours * 2) + 1)}>
                                        <span aria-hidden="true">&raquo;</span>
                                        <span className="sr-only">Next</span>
                                    </button>
                                </li>
                            );

                            return (
                                <li key={index} className={`page-item${this.currentPage === page ? ' active' : ''}`}>
                                    <button className="page-link" onClick={() => this.props.handlePagination(page)}>{page}</button>
                                </li>
                            );

                        })}

                    </ul>
                </nav>
            </Fragment>
        );
    }
}

Pagination.propTypes = {
    currentPage: PropTypes.number.isRequired,
    totalPages: PropTypes.number.isRequired,
    pageSize: PropTypes.number,
    pageNeighbours: PropTypes.number,
    handlePagination: PropTypes.func
};


export default Pagination;